using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevicesLib.Entities;

public class DeviceOIDDTO
{
    [Key]
    public Guid Id { get; set; }
    
    [Required]
    [ForeignKey(nameof(Device))]
    public Guid DeviceId { get; set; }
    public DeviceDTO Device { get; set; } = null!;
    
    [Required]
    public DeviceOIDType Type { get; set; }
    
    [Required]
    public string Value { get; set; } = null!;
    
    public enum DeviceOIDType
    {
        StorageDescription,
        StorageAllocationUnit,
        StorageTotal,
        StorageUsed,
    }
}