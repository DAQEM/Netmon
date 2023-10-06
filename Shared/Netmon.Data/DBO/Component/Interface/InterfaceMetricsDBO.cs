using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Netmon.Models.Component.Interface;
using Netmon.Models.Component.Interface.Metric;

namespace Netmon.Data.DBO.Component.Interface;

public class InterfaceMetricsDBO : IComponentMetricDBO
{
    [Key]
    public Guid Id { get; set; }
    
    [ForeignKey(nameof(Interface))]
    public Guid InterfaceId { get; set; }
    
    [Required]
    public DateTime Timestamp { get; set; }
    
    [Required]
    public InterfaceStatus AdminStatus { get; set; }
    
    [Required]
    public InterfaceStatus OperationStatus { get; set; }
    
    [Required]
    public uint Speed { get; set; }
    
    [Required]
    public long Mtu { get; set; }
    
    [Required]
    public ulong InOctets { get; set; }
    
    [Required]
    public ulong OutOctets { get; set; }
    
    [Required]
    public ulong InErrors { get; set; }
    
    [Required]
    public ulong OutErrors { get; set; }
    
    [Required]
    public ulong InDiscards { get; set; }
    
    [Required]
    public ulong OutDiscards { get; set; }
    
    [Required]
    public ulong InBroadcastPackets { get; set; }
    
    [Required]
    public ulong OutBroadcastPackets { get; set; }
    
    [Required]
    public ulong InMulticastPackets { get; set; }
    
    [Required]
    public ulong OutMulticastPackets { get; set; }
    
    [Required]
    public ulong InUnicastPackets { get; set; }
    
    [Required]
    public ulong OutUnicastPackets { get; set; }
    
    public InterfaceDBO Interface { get; set; } = null!;

    public static InterfaceMetricsDBO FromInterfaceMetric(IInterfaceMetric arg)
    {
        return new InterfaceMetricsDBO
        {
            Id = Guid.NewGuid(),
            Timestamp = arg.Timestamp,
            AdminStatus = arg.AdminStatus,
            OperationStatus = arg.OperationStatus,
            Speed = arg.Speed,
            Mtu = arg.Mtu,
            InOctets = arg.InOctets,
            OutOctets = arg.OutOctets,
            InErrors = arg.InErrors,
            OutErrors = arg.OutErrors,
            InDiscards = arg.InDiscards,
            OutDiscards = arg.OutDiscards,
            InBroadcastPackets = arg.InBroadcastPackets,
            OutBroadcastPackets = arg.OutBroadcastPackets,
            InMulticastPackets = arg.InMulticastPackets,
            OutMulticastPackets = arg.OutMulticastPackets,
            InUnicastPackets = arg.InUnicastPackets,
            OutUnicastPackets = arg.OutUnicastPackets
        };
    }
}