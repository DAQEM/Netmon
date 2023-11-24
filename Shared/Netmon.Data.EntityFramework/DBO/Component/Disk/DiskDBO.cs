using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Netmon.Data.EntityFramework.DBO.Device;
using Netmon.Models.Component.Disk;

namespace Netmon.Data.EntityFramework.DBO.Component.Disk;

public class DiskDBO : IComponentDBO
{
    [Key]
    public Guid Id { get; set; }
    
    [ForeignKey(nameof(Device))]
    public Guid DeviceId { get; set; }
    
    [Required]
    public int Index { get; set; }
    
    [Required]
    public string MountingPoint { get; set; } = null!;
    
    public List<DiskMetricsDBO> DiskMetrics { get; set; } = null!;

    public DeviceDBO Device { get; set; } = null!;

    public static DiskDBO FromDisk(IDisk? disk)
    {
        if (disk == null) return new DiskDBO();
        
        return new DiskDBO
        {
            Id = Guid.NewGuid(),
            Index = disk.Index,
            MountingPoint = disk.MountingPoint,
            DiskMetrics = disk.Metrics.Select(DiskMetricsDBO.FromDiskMetric).ToList()
        };
    }

    public IDisk ToDisk()
    {
        return new Models.Component.Disk.Disk
        {
            Index = Index,
            MountingPoint = MountingPoint,
            Metrics = DiskMetrics.Select(x => x.ToDiskMetric()).ToList()
        };
    }
}