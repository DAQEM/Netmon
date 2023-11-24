using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Netmon.Models.Component.Cpu.Core;

namespace Netmon.Data.EntityFramework.DBO.Component.Cpu.Core;

public class CpuCoreDBO : IComponentDBO
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

    public static CpuCoreDBO FromCpuCore(ICpuCore arg)
    {
        return new CpuCoreDBO
        {
            Id = Guid.NewGuid(),
            Index = arg.Index,
            Name = arg.Name,
            CpuCoreMetrics = arg.Metrics.Select(CpuCoreMetricsDBO.FromCpuCoreMetric).ToList()
        };
    }

    public ICpuCore ToCpuCore()
    {
        return new CpuCore
        {
            Index = Index,
            Name = Name,
            Metrics = CpuCoreMetrics.Select(x => x.ToCpuCoreMetric()).ToList()
        };
    }
}