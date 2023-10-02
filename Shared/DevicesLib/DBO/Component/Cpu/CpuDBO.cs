using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DevicesLib.DBO.Component.Cpu.Core;
using DevicesLib.DBO.Device;

namespace DevicesLib.DBO.Component.Cpu;

public class CpuDBO
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
}