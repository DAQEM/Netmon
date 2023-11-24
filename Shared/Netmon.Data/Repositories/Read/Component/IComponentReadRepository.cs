using Netmon.Models.Component;

namespace Netmon.Data.Repositories.Read.Component;

public interface IComponentReadRepository<T>: IReadRepository<T> where T: IComponent
{
    Task<List<T>> GetByDeviceId(Guid deviceId);
    
    Task<List<T>> GetByDeviceIdWithMetrics(Guid deviceId);
    Task<List<T>> GetByDeviceIdWithMetrics(Guid deviceId, DateTime from, DateTime to);
}