using DevicesLib.DBO.Component.Interface;

namespace DevicesLib.Repositories.Component.Interface;

public interface IInterfaceMetricsRepository
{
    public Task Add(InterfaceMetricsDBO interfaceMetrics);
}