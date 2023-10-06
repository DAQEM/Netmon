using Netmon.Models.Component.Memory.Metric;

namespace Netmon.Models.Component.Memory;

public class Memory : IMemory
{
    public int Index { get; set; }
    public string Name { get; set; }
    public List<IMemoryMetric> Metrics { get; set; }
}