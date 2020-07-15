using System.Threading.Tasks;

namespace SPMS.Application.Common.Interfaces
{
    public interface ITenantResolver
    {
        string GetHost();
        Task<string> GetHostAsync();
    }
}