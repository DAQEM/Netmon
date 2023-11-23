using Netmon.Data.DBO.Component.Memory;
using Netmon.Data.Repositories.Write.Component;
using Netmon.Models.Component.Memory.Metric;

namespace Netmon.Data.Services.Write.Component.Memory;

public interface IMemoryMetricsWriteService : IComponentMetricWriteService<MemoryMetric>
{
}