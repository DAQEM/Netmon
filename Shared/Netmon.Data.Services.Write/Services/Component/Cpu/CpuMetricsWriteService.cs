using Netmon.Data.DBO.Component.Cpu;
using Netmon.Data.Services.Write.Component.Cpu;
using Netmon.Models.Component.Cpu.Metric;

namespace Netmon.Data.Services.Write.Services.Component.Cpu;

public class CpuMetricsWriteService : ICpuMetricsWriteService
{
    public Task Add(CpuMetricsDBO metric)
    {
        throw new NotImplementedException();
    }

    public Task SaveChanges()
    {
        throw new NotImplementedException();
    }
}