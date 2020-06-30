namespace SPMS.Application.Common.Interfaces
{
    public interface ICurrentUserService
    {
        string GetAuthId();

        bool IsAuthenticated();
        string GetName();
        string GetEmail();
    }
}