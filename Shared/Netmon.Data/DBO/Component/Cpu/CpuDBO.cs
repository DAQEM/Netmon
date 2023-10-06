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

    public static CpuDBO FromCpu(ICpu arg)
    {
        return new CpuDBO
        {
            Id = Guid.NewGuid(),
            Index = arg.Index,
            CpuMetrics = arg.Metrics.Select(CpuMetricsDBO.FromCpuMetric).ToList(),
            CpuCores = arg.Cores.Select(CpuCoreDBO.FromCpuCore).ToList()
        };
    }
}