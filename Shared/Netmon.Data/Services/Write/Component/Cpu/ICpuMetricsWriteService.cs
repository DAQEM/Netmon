using Netmon.Data.Repositories.Write.Component;
using Netmon.Models.Component.Cpu.Metric;

namespace Netmon.Data.Services.Write.Component.Cpu;

public interface ICpuMetricsWriteService : IComponentMetricWriteRepository<ICpuMetric>
{
}