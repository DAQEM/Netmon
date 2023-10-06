using Netmon.Models.Component.Disk.Metric;

namespace Netmon.Models.Component.Disk;

public class Disk : IDisk
{
    public int Index { get; set; }
    public string MountingPoint { get; set; }
    public List<IDiskMetric> Metrics { get; set; }
}