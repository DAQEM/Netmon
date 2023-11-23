using Netmon.Data.DBO.Component.Disk;
using Netmon.Data.Repositories.Read.Component;
using Netmon.Models.Component.Disk.Metric;

namespace Netmon.Data.Services.Read.Component.Disk;

public interface IDiskMetricReadService : IComponentMetricReadService<DiskMetric>
{
    
}