using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.NetworkInformation;

namespace DevicesLib.DBO.Component.Interface;

public class InterfaceMetricsDBO
{
    [Key]
    public Guid Id { get; set; }
    
    [ForeignKey(nameof(Interface))]
    public Guid InterfaceId { get; set; }
    
    [Required]
    public DateTime Timestamp { get; set; }
    
    [Required]
    public uint Speed { get; set; }
    
    [Required]
    public PhysicalAddress PhysAddress { get; set; } = null!;
    
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
}