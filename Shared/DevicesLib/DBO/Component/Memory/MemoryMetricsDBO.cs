using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevicesLib.DBO.Component.Memory;

public class MemoryMetricsDBO
{
    [Key]
    public Guid Id { get; set; }
    
    [ForeignKey(nameof(Memory))]
    public Guid MemoryId { get; set; }
    
    [Required]
    public DateTime Timestamp { get; set; }
    
    [Required]
    public int AllocationUnits { get; set; }
    
    [Required]
    public int TotalSpace { get; set; }
    
    [Required]
    public int UsedSpace { get; set; }
    
    public MemoryDBO Memory { get; set; } = null!;
}