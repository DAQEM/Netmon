using Netmon.Data.DBO.Component.Cpu;
using Netmon.Models.Component.Cpu.Metric;

namespace Netmon.Data.Repositories.Read.Component.Cpu;

public interface ICpuMetricReadRepository : IComponentMetricReadRepository<CpuMetricsDBO>
{
}