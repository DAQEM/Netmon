using Netmon.Data.Services.Read.Component.Cpu;
using Netmon.Models.Component.Cpu.Metric;

namespace Netmon.Data.Services.Read.Services.Component.Cpu;

public class CpuMetricsReadService : ICpuMetricReadService
{
    
    public async Task<List<CpuMetric>> GetAll()
    {
        throw new NotImplementedException();
    }

    public async Task<CpuMetric?> GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<CpuMetric>> GetByComponentId(Guid componentId)
    {
        throw new NotImplementedException();
    }

    public async Task<List<CpuMetric>> GetByComponentIds(List<Guid> componentIds)
    {
        throw new NotImplementedException();
    }
}