using Netmon.Data.Services.Read.Component.Cpu;
using Netmon.Models.Component.Cpu.Metric;

namespace Netmon.Data.Services.Read.Services.Component.Cpu;

public class CpuMetricsReadService : ICpuMetricReadService
{
    
    public async Task<List<ICpuMetric>> GetAll()
    {
        throw new NotImplementedException();
    }

    public async Task<ICpuMetric?> GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<ICpuMetric>> GetByComponentId(Guid componentId)
    {
        throw new NotImplementedException();
    }

    public async Task<List<ICpuMetric>> GetByComponentIds(List<Guid> componentIds)
    {
        throw new NotImplementedException();
    }
}