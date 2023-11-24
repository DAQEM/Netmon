using Netmon.Models.Component;

namespace Netmon.Data.Repositories.Read.Component;

public interface IComponentMetricReadRepository<T> : IReadRepository<T> where T : IComponentMetric
{
    Task<List<T>> GetByComponentId(Guid componentId);
    Task<List<T>> GetByComponentIds(List<Guid> componentIds);
}