using Netmon.Data.DBO.Component.Memory;
using Netmon.Data.Services.Read.Component.Memory;
using Netmon.Models.Component.Memory.Metric;

namespace Netmon.Data.Services.Read.Services.Component.Memory;

public class MemoryMetricsReadService : IMemoryMetricReadService
{
    
    public async Task<List<MemoryMetric>> GetAll()
    {
        throw new NotImplementedException();
    }

    public async Task<MemoryMetric?> GetById(Guid id)
    {
        throw new NotImplementedException();
    }
    
    public async Task<List<MemoryMetric>> GetByComponentId(Guid componentId)
    {
        throw new NotImplementedException();
    }

    public async Task<List<MemoryMetric>> GetByComponentIds(List<Guid> componentIds)
    {
        throw new NotImplementedException();
    }
}