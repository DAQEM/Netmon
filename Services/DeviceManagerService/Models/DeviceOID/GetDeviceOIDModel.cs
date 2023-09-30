namespace DeviceManagerService.Models.DeviceOID;

public class GetDeviceOIDModel
{
    public Guid Id { get; set; }
    public Guid DeviceId { get; set; }
    public DevicesLib.Entities.DeviceOIDDTO.DeviceOIDType Type { get; set; }
    public string Value { get; set; }
}