using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.NetworkInformation;
using Netmon.Data.DBO.Device;
using Netmon.Models.Component.Interface;

namespace Netmon.Data.DBO.Component.Interface;

public class InterfaceDBO : IComponentDBO
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

    public static InterfaceDBO FromInterface(IInterface arg)
    {
        return new InterfaceDBO
        {
            Id = Guid.NewGuid(),
            Index = arg.Index,
            Name = arg.Name,
            Type = arg.Type,
            PhysAddress = arg.PhysAddress,
            InterfaceMetrics = arg.Metrics.Select(InterfaceMetricsDBO.FromInterfaceMetric).ToList()
        };
    }
}