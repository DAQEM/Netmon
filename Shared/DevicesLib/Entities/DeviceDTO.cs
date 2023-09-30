using System.ComponentModel.DataAnnotations;
using DevicesLib.Attribute.Validator;

namespace DevicesLib.Entities;

public class DeviceDTO
{
    [Key]
    public Guid Id { get; set; }
    
    [Required]
    public string Name { get; set; } = null!;
    
    [Required]
    public string Description { get; set; } = null!;
    
    [Required]
    [ValidateIpAddress]
    public string IpAddress { get; set; } = null!;
    
    [Required]
    public int Port { get; set; } = 161;
    
    [Required]
    public DateTime LastPoll { get; set; } = DateTime.Now;
    
    [Required]
    public string Community { get; set; } = null!;
    
    public string? Location { get; set; }
    
    public string? Contact { get; set; }
    
    
    
    public List<DeviceOIDDTO> OIDs { get; set; } = new();
    public List<DiskDTO> Disks { get; set; } = new();
}