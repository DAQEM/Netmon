using DevicesLib.DBO.Component.Memory;

namespace DevicesLib.Repositories.Component.Memory;

public interface IMemoryMetricsRepository
{
    public Task Add(MemoryMetricsDBO memoryMetrics);
}