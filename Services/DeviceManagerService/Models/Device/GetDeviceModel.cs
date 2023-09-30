namespace DeviceManagerService.Models.Device;

public class GetDeviceModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string IpAddress { get; set; }
    public string Community { get; set; }
    public string? Location { get; set; }
    public string? Contact { get; set; }
}