using DevicesLib.DBO.Component.Cpu.Core;

namespace DevicesLib.Repositories.Component.Cpu.Core;

public interface ICpuCoreMetricsRepository
{
    public Task Add(CpuCoreMetricsDBO cpuCoreMetrics);
}