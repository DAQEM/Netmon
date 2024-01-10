using Netmon.Data.DBO.Component.Cpu.Core;
using Netmon.Models.Component.Cpu.Core.Metric;

namespace Netmon.Data.Repositories.Write.Component.Cpu.Core;

public interface ICpuCoreMetricsWriteRepository : IComponentMetricWriteRepository<CpuCoreMetricsDBO>
{
}