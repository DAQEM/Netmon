using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevicesLib.DBO.Component.Disk;

public class DiskMetricsDBO
{
    [Key]
    public Guid Id { get; set; }
    
    [ForeignKey(nameof(Disk))]
    public Guid DiskId { get; set; }
    
    [Required]
    public DateTime Timestamp { get; set; }
    
    [Required]
    public int AllocationUnits { get; set; }
    
    [Required]
    public int TotalSpace { get; set; }
    
    [Required]
    public int UsedSpace { get; set; }
    
    public DiskDBO Disk { get; set; } = null!;
}