using Netmon.Models.Component.Disk.Metric;

namespace Netmon.Models.Component.Disk;

public interface IDisk : IComponent
{
    public int Index { get; }
    public string MountingPoint { get; }
    public List<IDiskMetric> Metrics { get; set; }
}