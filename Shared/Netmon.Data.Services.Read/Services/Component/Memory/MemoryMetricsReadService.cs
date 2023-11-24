using Netmon.Data.Services.Read.Component.Memory;
using Netmon.Models.Component.Memory.Metric;

namespace Netmon.Data.Services.Read.Services.Component.Memory;

public class MemoryMetricsReadService : IMemoryMetricReadService
{
    
    public async Task<List<IMemoryMetric>> GetAll()
    {
        throw new NotImplementedException();
    }

    public async Task<IMemoryMetric?> GetById(Guid id)
    {
        throw new NotImplementedException();
    }
    
    public async Task<List<IMemoryMetric>> GetByComponentId(Guid componentId)
    {
        throw new NotImplementedException();
    }

    public async Task<List<IMemoryMetric>> GetByComponentIds(List<Guid> componentIds)
    {
        throw new NotImplementedException();
    }
}