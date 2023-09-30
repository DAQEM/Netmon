using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevicesLib.Entities;

public class DiskDTO
{
    [Key]
    public Guid Id { get; set; }
    
    [Required]
    [ForeignKey(nameof(Device))]
    public Guid DeviceId { get; set; }

    [Required]
    public string Name { get; set; } = null!;
    
    public string? Description { get; set; }
    
    [Required]
    public string MountingPoint { get; set; } = null!;
    
    [Required]
    public long TotalSpace { get; set; }
    
    [Required]
    public long AllocationUnits { get; set; }
    
    [Required]
    public long UsedSpace { get; set; }



    public DeviceDTO? Device { get; set; }
    public List<DiskMetricDTO>? Metrics { get; set; }
}