using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.ApplicationInsights.WindowsServer;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SPMS.Application.Common.Interfaces;
using SPMS.Application.Dtos;
using SPMS.Common;
using SPMS.Domain.Models;

namespace SPMS.Application.Common.Provider
{
    public class TenantProvider : ITenantProvider<TenantDto>
    {
        private readonly ISpmsContext _db;
        private readonly IMapper _mapper;
        private TenantDto _tenant;

        public TenantProvider(ISpmsContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;

        }

        public async Task<TenantDto> GetTenantAsync(string url, CancellationToken cancellationToken)
        {
            return _tenant ??= await _db.Game.Include(gd => gd.Url).Where(x => x.Url.Any(y => y.Url == url))
                                   .ProjectTo<TenantDto>(_mapper.ConfigurationProvider)
                                   .FirstOrDefaultAsync(cancellationToken: cancellationToken) ??
                               await _db.Game.Include(g => g.Url)
                                   .Where(gm => gm.Name == StaticValues.TestGame)
                                   .ProjectTo<TenantDto>(_mapper.ConfigurationProvider)
                                   .FirstOrDefaultAsync(cancellationToken);
        }

        //TODO Make this extension method off of HttpContext
        public async Task<string> ProtectIdAsync(int id, CancellationToken cancellationToken)
        {
            if(_tenant == null)
                await GetTenantAsync(string.Empty, cancellationToken);
            return HideIdentifier(id.ToString());
        }

        public async Task<int> UnprotectAsync(string identifier, CancellationToken cancellationToken)
        {
            if (_tenant == null)
                await GetTenantAsync(string.Empty, cancellationToken);
            return int.Parse(RevealIdentifier(identifier));
        }

        public string RevealIdentifier(string hidden)
        {
            Span<byte> data = SimpleBase.Base58.Bitcoin.Decode(hidden);
            byte[] nonce = data.Slice(0, 12).ToArray();
            byte[] encrypted = data.Slice(12).ToArray();
            byte[] plain = Sodium.SecretAeadAes.Decrypt(encrypted, nonce, _tenant.GameKey);
            return Encoding.UTF8.GetString(plain);

        }
        public string HideIdentifier(string id)
        {
            byte[] nonce = Sodium.SecretAeadAes.GenerateNonce();
            byte[] encrypted = Sodium.SecretAeadAes.Encrypt(Encoding.UTF8.GetBytes(id), nonce, _tenant.GameKey);

            return SimpleBase.Base58.Bitcoin.Encode(nonce.Concat(encrypted).ToArray());
        }
    }
}
