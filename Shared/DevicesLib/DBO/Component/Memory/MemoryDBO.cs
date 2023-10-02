using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DevicesLib.DBO.Device;

namespace DevicesLib.DBO.Component.Memory;

public class MemoryDBO
{
    [Key]
    public Guid Id { get; set; }
    
    [ForeignKey(nameof(Device))]
    public Guid DeviceId { get; set; }
    
    [Required]
    public int Index { get; set; }
    
    [Required]
    public string Name { get; set; } = null!;
    
    public List<MemoryMetricsDBO> MemoryMetrics { get; set; } = null!;
    
    public DeviceDBO Device { get; set; } = null!;
}