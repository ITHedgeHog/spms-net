using System;
using System.Linq;
using System.Text;

namespace SPMS.Application.Services
{
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
            return int.Parse((string) RevealIdentifier(identifier));
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
