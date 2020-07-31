namespace SPMS.Application.Common.Interfaces
{
    public interface IIdentifierMask
    {
        int RevealId(string identifier, byte[] key);
        int RevealId(string identifier);
        string HideId(int id);
        string HideId(int id, byte[] key);
    }

    public interface IBackgroundIdentifierMask
    {
        void SetKey(byte[] key);
        int RevealId(string identifier);
        string HideId(int id);
    }
}