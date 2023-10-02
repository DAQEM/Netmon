using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevicesLib.DBO.Component.Cpu;

public class CpuMetricsDBO
{
    [Key]
    public Guid Id { get; set; }
    
    [ForeignKey(nameof(Cpu))]
    public Guid CpuId { get; set; }
    
    [Required]
    public DateTime Timestamp { get; set; }
    
    [Required]
    public int OneMinuteLoad { get; set; }
    
    [Required]
    public int FiveMinuteLoad { get; set; }
    
    [Required]
    public int FifteenMinuteLoad { get; set; }
    
    public CpuDBO Cpu { get; set; } = null!;
}