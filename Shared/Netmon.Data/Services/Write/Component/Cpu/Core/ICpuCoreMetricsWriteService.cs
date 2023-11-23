using Netmon.Data.DBO.Component.Cpu.Core;
using Netmon.Data.Repositories.Write.Component;
using Netmon.Models.Component.Cpu.Core.Metric;

namespace Netmon.Data.Services.Write.Component.Cpu.Core;

public interface ICpuCoreMetricsWriteService : IComponentMetricWriteService<CpuCoreMetric>
{
}