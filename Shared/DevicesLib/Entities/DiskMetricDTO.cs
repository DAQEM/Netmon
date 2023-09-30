using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevicesLib.Entities;

public class DiskMetricDTO
{
    [Key]
    public Guid Id { get; set; }
    
    [Required]
    [ForeignKey(nameof(Disk))]
    public Guid DiskId { get; set; }
    public DiskDTO Disk { get; set; } = null!;
    
    [Required]
    public DateTime Timestamp { get; set; }
    
    [Required]
    public DiskMetricType Type { get; set; }
    
    [Required]
    public long Value { get; set; }
    
    public enum DiskMetricType 
    {
        UsedSpace
    }
}