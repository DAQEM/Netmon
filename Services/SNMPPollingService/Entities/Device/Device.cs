using DevicesLib.Entities;
using SNMPPollingService.Entities.Component;

namespace SNMPPollingService.Entities.Device;

public class Device : IDevice
{
    public Device(DeviceDTO device)
    {
        IpAddress = device.IpAddress;
        Port = device.Port;
        Community = device.Community;
        Name = device.Name;
        Location = device.Location;
        Contact = device.Contact;
    }

    public Device(string ipAddress, int port, string community, string? name = null, string? location = null, string? contact = null)
    {
        IpAddress = ipAddress;
        Port = port;
        Community = community;
        Name = name;
        Location = location;
        Contact = contact;
    }

    public string IpAddress { get; private set; }
    public int Port { get; private set; }
    public string Community { get; private set; }
    public string? Name { get; private set; }
    public string? Location { get; private set; }
    public string? Contact { get; private set; }
    public Dictionary<EntityType, List<IComponent>> Components { get; } = new();
}