using Netmon.Data.DBO.Component.Disk;
using Netmon.Data.Repositories.Write.Component;
using Netmon.Models.Component.Disk.Metric;

namespace Netmon.Data.Services.Write.Component.Disk;

public interface IDiskMetricsWriteService : IComponentMetricWriteService<DiskMetric>
{
}