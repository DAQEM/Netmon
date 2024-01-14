using Netmon.Data.Services.Read.Component.Cpu.Core;
using Netmon.Models.Component.Cpu.Core.Metric;

namespace Netmon.Data.Services.Read.Services.Component.Cpu.Core;

public class CpuCoreMetricsReadService : ICpuCoreMetricReadService
{

    public Task<List<ICpuCoreMetric>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<ICpuCoreMetric?> GetById(Guid id)
    {
        throw new NotImplementedException();
    }
}