using System.Threading;
using System.Threading.Tasks;
using SPMS.Domain.Models;

namespace SPMS.Application.Common.Interfaces
{
    public interface IUserService
    {
        string GetAuthId();
        string GetName();
        bool IsPlayer();
        bool IsAdmin();
        int GetId();
        Player GetPlayerFromDatabase();
        Task<string> GetEmailAsync(CancellationToken token);
        bool IsAuthenticated();
        Task CreateNewPlayer(CancellationToken cancellationToken);
    }
}
