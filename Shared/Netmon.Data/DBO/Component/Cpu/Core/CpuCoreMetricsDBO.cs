using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Netmon.Models.Component.Cpu.Core.Metric;

namespace Netmon.Data.DBO.Component.Cpu.Core;

public class CpuCoreMetricsDBO : IComponentMetricDBO
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

    public static CpuCoreMetricsDBO FromCpuCoreMetric(ICpuCoreMetric arg)
    {
        return new CpuCoreMetricsDBO
        {
            Id = Guid.NewGuid(),
            Timestamp = arg.Timestamp,
            Load = arg.Load
        };
    }
}