using Netmon.Data.DBO.Component;
using Netmon.Data.Repositories.Read;
using Netmon.Models.Component;

namespace Netmon.Data.Services.Read.Component;

public interface IComponentMetricReadService<T> : IReadService<T> where T : IComponentMetric
{
    Task<List<T>> GetByComponentId(Guid componentId);
    Task<List<T>> GetByComponentIds(List<Guid> componentIds);
}