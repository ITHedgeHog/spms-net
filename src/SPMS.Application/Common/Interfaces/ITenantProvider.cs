using System.Threading;
using System.Threading.Tasks;
using SPMS.Domain.Models;

namespace SPMS.Application.Common.Interfaces
{
    public interface ITenantProvider
    {
        Task<Game> GetTenantAsync(CancellationToken cancellationToken);
        Task<string> ProtectIdAsync(int id, CancellationToken cancellationToken);
        Task<int> UnprotectAsync(string identifier, CancellationToken cancellationToken);
    }
}