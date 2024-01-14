using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Netmon.Data.DBO.Component.Cpu.Core;
using Netmon.Data.DBO.Device;
using Netmon.Models.Component.Cpu;

namespace Netmon.Data.DBO.Component.Cpu;

public class CpuDBO : IComponentDBO
{
    [Key]
    public Guid Id { get; set; }
    
    [ForeignKey(nameof(Device))]
    public Guid DeviceId { get; set; }
    
    [Required]
    public int Index { get; set; }
    
    public List<CpuMetricsDBO> CpuMetrics { get; set; } = null!;
    public List<CpuCoreDBO> CpuCores { get; set; } = null!;
    
    public DeviceDBO Device { get; set; } = null!;
    
    public static CpuDBO FromCpu(ICpu? cpu)
    {
        if (cpu is null) return new CpuDBO();
        return new CpuDBO
        {
            Id = Guid.NewGuid(),
            Index = cpu.Index,
            CpuMetrics = cpu.Metrics.Select(CpuMetricsDBO.FromCpuMetric).ToList(),
            CpuCores = cpu.Cores.Select(CpuCoreDBO.FromCpuCore).ToList()
        };
    }

    public ICpu ToCpu()
    {
        return new Models.Component.Cpu.Cpu
        {
            Index = Index,
            Metrics = CpuMetrics.Select(x => x.ToCpuMetric()).ToList(),
            Cores = CpuCores.Select(x => x.ToCpuCore()).ToList()
        };
    }
}