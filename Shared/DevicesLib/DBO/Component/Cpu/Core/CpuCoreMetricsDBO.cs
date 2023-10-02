using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevicesLib.DBO.Component.Cpu.Core;

public class CpuCoreMetricsDBO
{
    [Key]
    public Guid Id { get; set; }
    
    [ForeignKey(nameof(CpuCore))]
    public Guid CpuCoreId { get; set; }
    
    [Required]
    public DateTime Timestamp { get; set; }
    
    [Required]
    public int Load { get; set; }
    
    public CpuCoreDBO CpuCore { get; set; } = null!;
}