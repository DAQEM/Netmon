using Netmon.Data.DBO.Component.Cpu;
using Netmon.Models.Component.Cpu.Metric;

namespace Netmon.Data.Repositories.Write.Component.Cpu;

public interface ICpuMetricsWriteRepository : IComponentMetricWriteRepository<CpuMetricsDBO>
{
}