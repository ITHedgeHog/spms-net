using SPMS.Application.Dtos;

namespace SPMS.Application.Common.Interfaces
{
    public interface ITenantAccessor<T> where T : TenantDto
    {
        T Instance { get; }
    }
}