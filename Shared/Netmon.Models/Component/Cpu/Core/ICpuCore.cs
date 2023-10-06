using Netmon.Models.Component.Cpu.Core.Metric;

namespace Netmon.Models.Component.Cpu.Core;

public interface ICpuCore
{
    public int Index { get; }
    public string Name { get; set; }
    public List<ICpuCoreMetric> Metrics { get; set; }
    
}