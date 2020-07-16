using SPMS.Application.Dtos;

namespace SPMS.Application.Common.Interfaces
{
    public interface ITenantAccessor<out T> where T : TenantDto
    {
        T Instance { get; }
    }
}