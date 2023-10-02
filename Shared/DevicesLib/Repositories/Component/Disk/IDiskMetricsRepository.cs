using DevicesLib.DBO.Component.Disk;

namespace DevicesLib.Repositories.Component.Disk;

public interface IDiskMetricsRepository
{
    public Task Add(DiskMetricsDBO diskMetrics);
}