using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevicesLib.DBO.Device;

public class DeviceConnectionDBO
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
    [DefaultValue("V2")]
    public string SNMPVersion { get; set; } = null!;
    
    [Required]
    [DefaultValue("")]
    public string AuthPassword { get; set; } = null!;
    
    [Required]
    [DefaultValue("")]
    public string PrivacyPassword { get; set; } = null!;
    
    [Required]
    [DefaultValue("")]
    public string AuthProtocol { get; set; } = null!;
    
    [Required]
    [DefaultValue("")]
    public string PrivacyProtocol { get; set; } = null!;
    
    [Required]
    [DefaultValue("")]
    public string ContextName { get; set; } = null!;
    
    public DeviceDBO Device { get; set; } = null!;
    
}