using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Netmon.Data.DBO.Component;
using Netmon.Models.Device.Connection;
using Netmon.Models.Device.Connection.Protocol;

namespace Netmon.Data.DBO.Device;

public class DeviceConnectionDBO : IComponentDBO
{
    [Key]
    public Guid Id { get; set; }
    
    [ForeignKey(nameof(DeviceDBO))]
    public Guid DeviceId { get; set; }
    
    [Required]
    [DefaultValue(161)]
    public int Port { get; set; }
    
    [Required]
    [DefaultValue("public")]
    public string Community { get; set; } = null!;
    
    [Required]
    [DefaultValue(1)]
    public int SNMPVersion { get; set; }
    
    [Required]
    [DefaultValue("")]
    public string AuthPassword { get; set; } = null!;
    
    [Required]
    [DefaultValue("")]
    public string PrivacyPassword { get; set; } = null!;
    
    [Required]
    [DefaultValue("")]
    public AuthProtocol AuthProtocol { get; set; }
    
    [Required]
    [DefaultValue("")]
    public PrivacyProtocol PrivacyProtocol { get; set; }
    
    [Required]
    [DefaultValue("")]
    public string ContextName { get; set; } = null!;
    
    public DeviceDBO Device { get; set; } = null!;

    public static DeviceConnectionDBO FromDeviceConnection(IDeviceConnection? deviceConnection)
    {
        if (deviceConnection == null)
        {
            return new DeviceConnectionDBO();
        }
        
        return new DeviceConnectionDBO
        {
            Id = Guid.NewGuid(),
            Port = deviceConnection.Port,
            Community = deviceConnection.Community,
            SNMPVersion = deviceConnection.SNMPVersion,
            AuthPassword = deviceConnection.AuthPassword,
            PrivacyPassword = deviceConnection.PrivacyPassword,
            AuthProtocol = deviceConnection.AuthProtocol,
            PrivacyProtocol = deviceConnection.PrivacyProtocol,
            ContextName = deviceConnection.ContextName
        };
    }
}