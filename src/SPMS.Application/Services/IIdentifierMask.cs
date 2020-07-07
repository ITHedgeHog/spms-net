namespace SPMS.Application.Services
{
    public interface IIdentifierMask
    {
        int RevealId(string identifier);
        string HideId(int id);
    }
}