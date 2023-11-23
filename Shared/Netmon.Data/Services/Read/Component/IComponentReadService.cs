using Netmon.Models.Component;

namespace Netmon.Data.Services.Read.Component;

public interface IComponentReadService<T>: IReadService<T> where T: IComponent
{
    Task<List<T>> GetByDeviceId(Guid deviceId);
    
    Task<List<T>> GetByDeviceIdWithMetrics(Guid deviceId);
    Task<List<T>> GetByDeviceIdWithMetrics(Guid deviceId, DateTime from, DateTime to);
}