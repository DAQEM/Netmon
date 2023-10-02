using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevicesLib.DBO.Component.Cpu.Core;

public class CpuCoreDBO
{
    [Key]
    public Guid Id { get; set; }
    
    [ForeignKey(nameof(Cpu))]
    public Guid CpuId { get; set; }
    
    [Required]
    public int Index { get; set; }
    
    [Required]
    public string Name { get; set; } = null!;
    
    public List<CpuCoreMetricsDBO> CpuCoreMetrics { get; set; } = null!;
    
    public CpuDBO Cpu { get; set; } = null!;
}