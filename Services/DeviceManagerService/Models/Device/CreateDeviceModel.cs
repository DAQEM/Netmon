using System.Net;
using DevicesLib.Attribute.Validator;
using DevicesLib.Entities;

namespace DeviceManagerService.Models.Device;

public class CreateDeviceModel
{
    public string Name { get; set; } = null!;
    
    public string Description { get; set; } = null!;
    
    [ValidateIpAddress]
    public string IpAddress { get; set; } = null!;
    
    public string Community { get; set; } = null!;
    
    public string? Location { get; set; }
    
    public string? Contact { get; set; }
}