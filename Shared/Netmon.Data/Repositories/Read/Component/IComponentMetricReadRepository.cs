using Netmon.Data.DBO.Component;

namespace Netmon.Data.Repositories.Read.Component;

public interface IComponentMetricReadRepository<T> : IReadRepository<T> where T : IComponentMetricDBO
{
    Task<List<T>> GetByComponentId(Guid componentId);
    Task<List<T>> GetByComponentIds(List<Guid> componentIds);
}