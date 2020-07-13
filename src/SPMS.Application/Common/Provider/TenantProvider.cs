using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights.WindowsServer;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SPMS.Application.Common.Interfaces;
using SPMS.Common;
using SPMS.Domain.Models;

namespace SPMS.Application.Common.Provider
{
    public class TenantProvider : ITenantProvider
    {
        private readonly ISpmsContext _db;
        private readonly string _url;
        private Game _game;

        public TenantProvider(ISpmsContext db, IHostProvider host)
        {
            _db = db;
            _url = host.GetHost();

        }

        public async Task<Game> GetTenantAsync(CancellationToken cancellationToken)
        {
            if (_game == null)
            {
                _game = await _db.Game.Include(gd => gd.Url).Where(x => x.Url.Any(y => y.Url == _url))
                            .FirstOrDefaultAsync(cancellationToken: cancellationToken) ??
                       await  _db.Game.Include(g => g.Url).FirstAsync(gm => gm.Name == StaticValues.TestGame,
                            cancellationToken: cancellationToken);
            }
            return _game;
        }

        public async Task<string> ProtectIdAsync(int id, CancellationToken cancellationToken)
        {
            if(_game == null)
                await GetTenantAsync(cancellationToken);
            return HideIdentifier(id.ToString());
        }

        public async Task<int> UnprotectAsync(string identifier, CancellationToken cancellationToken)
        {
            if (_game == null)
                await GetTenantAsync(cancellationToken);
            return int.Parse(RevealIdentifier(identifier));
        }

        public string RevealIdentifier(string hidden)
        {
            Span<byte> data = SimpleBase.Base58.Bitcoin.Decode(hidden);
            byte[] nonce = data.Slice(0, 12).ToArray();
            byte[] encrypted = data.Slice(12).ToArray();
            byte[] plain = Sodium.SecretAeadAes.Decrypt(encrypted, nonce, _game.GameKey);
            return Encoding.UTF8.GetString(plain);

        }
        public string HideIdentifier(string id)
        {
            byte[] nonce = Sodium.SecretAeadAes.GenerateNonce();
            byte[] encrypted = Sodium.SecretAeadAes.Encrypt(Encoding.UTF8.GetBytes(id), nonce, _game.GameKey);

            return SimpleBase.Base58.Bitcoin.Encode(nonce.Concat(encrypted).ToArray());
        }
    }
}
