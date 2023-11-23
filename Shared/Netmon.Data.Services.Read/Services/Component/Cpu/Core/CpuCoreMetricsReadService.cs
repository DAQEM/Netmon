using Netmon.Data.Repositories.Read.Component.Cpu.Core;
using Netmon.Data.Services.Read.Component.Cpu.Core;
using Netmon.Models.Component.Cpu.Core.Metric;

namespace Netmon.Data.Services.Read.Services.Component.Cpu.Core;

public class CpuCoreMetricsReadService : ICpuCoreMetricReadService
{
    public readonly ICpuCoreMetricReadRepository _repository;
    
    public CpuCoreMetricsReadService(ICpuCoreMetricReadRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<List<CpuCoreMetric>> GetAll()
    {
        throw new NotImplementedException();
    }

    public async Task<CpuCoreMetric?> GetById(Guid id)
    {
        throw new NotImplementedException();
    }
}