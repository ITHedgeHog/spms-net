using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using SPMS.Application.Services;

namespace SPMS.Web
{

    public interface IIdentifierMask
    {
        int RevealId(string identifier);
        string HideId(int id);
    }
    public class IdentifierMasking : IIdentifierMask
    {
        private static byte[] _key;

        public IdentifierMasking(IGameService game)
        {
            var key = game.GetGameKey(Sodium.SecretBox.GenerateKey());
            _key = key;
        }

        public int RevealId(string identifier)
        {
            return int.Parse(RevealIdentifier(identifier));
        }

        public string HideId(int id)
        {
            return HideIdentifier(id.ToString());
        }

        public string RevealIdentifier(string hidden)
        {
            Span<byte> data = SimpleBase.Base58.Bitcoin.Decode(hidden);
            byte[] nonce = data.Slice(0, 12).ToArray();
            byte[] encrypted = data.Slice(12).ToArray();
            byte[] plain = Sodium.SecretAeadAes.Decrypt(encrypted, nonce, _key);
            return Encoding.UTF8.GetString(plain);

        }
        public string HideIdentifier(string id)
        {
            byte[] nonce = Sodium.SecretAeadAes.GenerateNonce();
            byte[] encrypted = Sodium.SecretAeadAes.Encrypt(Encoding.UTF8.GetBytes(id), nonce, _key);

            return SimpleBase.Base58.Bitcoin.Encode(nonce.Concat(encrypted).ToArray());
        }
    }
}
