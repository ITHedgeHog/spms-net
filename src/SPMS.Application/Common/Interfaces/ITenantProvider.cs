using System.Threading;
using System.Threading.Tasks;
using SPMS.Domain.Models;

namespace SPMS.Application.Common.Interfaces
{
    public interface ITenantProvider
    {
        Task<Game> GetTenantAsync(string url, CancellationToken cancellationToken);
    }
}