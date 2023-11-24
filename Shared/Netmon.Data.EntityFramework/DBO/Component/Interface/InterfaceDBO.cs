using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.NetworkInformation;
using Netmon.Data.EntityFramework.DBO.Device;
using Netmon.Models.Component.Interface;

namespace Netmon.Data.EntityFramework.DBO.Component.Interface;

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

    public static InterfaceDBO FromInterface(IInterface? @interface)
    {
        if (@interface == null) return new InterfaceDBO();
        return new InterfaceDBO
        {
            Id = Guid.NewGuid(),
            Index = @interface.Index,
            Name = @interface.Name,
            Type = @interface.Type,
            PhysAddress = @interface.PhysAddress,
            InterfaceMetrics = @interface.Metrics.Select(InterfaceMetricsDBO.FromInterfaceMetric).ToList()
        };
    }

    public IInterface ToInterface()
    {
        return new Models.Component.Interface.Interface
        {
            Index = Index,
            Name = Name,
            Type = Type,
            PhysAddress = PhysAddress,
            Metrics = InterfaceMetrics.Select(x => x.ToInterfaceMetric()).ToList()
        };
    }
}