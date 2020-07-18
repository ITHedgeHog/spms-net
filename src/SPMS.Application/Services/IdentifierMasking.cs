using System;
using System.Linq;
using System.Text;
using SPMS.Application.Common.Interfaces;
using SPMS.Application.Dtos;

namespace SPMS.Application.Services
{
    public class IdentifierMasking : IIdentifierMask
    {
        private static byte[] _key;

        public IdentifierMasking(ITenantAccessor<TenantDto> tenant)
        {

            _key = tenant.Instance.GameKey;
        }

        public IdentifierMasking()
        {

        }

        public int RevealId(string identifier)
        {
            return int.Parse((string) RevealIdentifier(identifier));
        }

        public int RevealId(string identifier, byte[] key)
        {
            _key = key;
            return int.Parse((string)RevealIdentifier(identifier));
        }

        public string HideId(int id)
        {
            return HideIdentifier(id.ToString());
        }

        public string HideId(int id, byte[] key)
        {
            _key = key;
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
