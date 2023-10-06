using Netmon.Models.Component.Memory.Metric;

namespace Netmon.Models.Component.Memory;

public interface IMemory : IComponent
{
    public int Index { get; }
    public string Name { get; }
    public List<IMemoryMetric> Metrics { get; set; }
}