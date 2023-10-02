using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DevicesLib.DBO.Device;

namespace DevicesLib.DBO.Component.Disk;

public class DiskDBO
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
}