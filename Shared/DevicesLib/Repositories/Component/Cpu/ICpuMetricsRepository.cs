using DevicesLib.DBO.Component.Cpu;

namespace DevicesLib.Repositories.Component.Cpu;

public interface ICpuMetricsRepository
{
    public Task Add(CpuMetricsDBO cpuMetrics);
}