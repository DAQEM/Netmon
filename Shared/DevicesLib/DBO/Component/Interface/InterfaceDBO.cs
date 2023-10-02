using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.NetworkInformation;
using DevicesLib.DBO.Device;
using DevicesLib.Entities.Component.Interface;

namespace DevicesLib.DBO.Component.Interface;

public class InterfaceDBO
{
    [Key]
    public Guid Id { get; set; }
    
    [ForeignKey(nameof(Device))]
    public Guid DeviceId { get; set; }
    
    [Required]
    public int Index { get; set; }
    
    [Required]
    public string Name { get; set; } = null!;
    
    [Required]
    public InterfaceType Type { get; set; }
    
    [Required]
    public PhysicalAddress PhysAddress { get; set; } = null!;
    
    public List<InterfaceMetricsDBO> InterfaceMetrics { get; set; } = null!;
    
    public DeviceDBO Device { get; set; } = null!;
}