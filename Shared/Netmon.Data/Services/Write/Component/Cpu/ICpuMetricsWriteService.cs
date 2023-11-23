using Netmon.Data.DBO.Component.Cpu;
using Netmon.Data.Repositories.Write.Component;

namespace Netmon.Data.Services.Write.Component.Cpu;

public interface ICpuMetricsWriteService : IComponentMetricWriteRepository<CpuMetricsDBO>
{
}