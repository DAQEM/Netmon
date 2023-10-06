using Netmon.Models.Component.Cpu.Core.Metric;

namespace Netmon.Models.Component.Cpu.Core;

public class CpuCore : ICpuCore
{
    public int Index { get; set; }
    public string Name { get; set; }
    public List<ICpuCoreMetric> Metrics { get; set; }
}