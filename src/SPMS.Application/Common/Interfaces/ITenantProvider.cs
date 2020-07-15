using System.Threading;
using System.Threading.Tasks;
using SPMS.Application.Dtos;

namespace SPMS.Application.Common.Interfaces
{
    public interface ITenantProvider<T> where T : TenantDto
    {
         
        Task<T> GetTenantAsync(string _url, CancellationToken cancellationToken);
        Task<string> ProtectIdAsync(int id, CancellationToken cancellationToken);
        Task<int> UnprotectAsync(string identifier, CancellationToken cancellationToken);
    }
}