using System.Text.Json.Serialization;
using Netmon.Data.DBO.Device;
using Netmon.Models.Attribute.Validator;
using Netmon.Models.Device;

namespace Netmon.DeviceManager.DTO.Device;

public class DeviceWithConnectionDTO
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    [ValidateIpAddress]
    public string? IpAddress { get; set; }
    public string? Location { get; set; }
    public string? Contact { get; set; }
    public DeviceConnectionDTO? Connection { get; set; }

    public static DeviceWithConnectionDTO FromDeviceDBO(DeviceDBO deviceDBO)
    {
        return new DeviceWithConnectionDTO
        {
            Id = deviceDBO.Id,
            Name = deviceDBO.Name,
            IpAddress = deviceDBO.IpAddress,
            Location = deviceDBO.Location,
            Contact = deviceDBO.Contact
        };
    }
    
    public static DeviceWithConnectionDTO FromDeviceDBOWithConnection(DeviceDBO deviceDBO)
    {
        return new DeviceWithConnectionDTO
        {
            Id = deviceDBO.Id,
            Name = deviceDBO.Name,
            IpAddress = deviceDBO.IpAddress,
            Location = deviceDBO.Location,
            Contact = deviceDBO.Contact,
            Connection = DeviceConnectionDTO.FromDeviceConnectionDBO(deviceDBO.DeviceConnection)
        };
    }
    
    public static DeviceWithConnectionDTO FromDeviceWithConnection(IDevice device)
    {
        return new DeviceWithConnectionDTO
        {
            Id = device.Id,
            Name = device.Name,
            IpAddress = device.IpAddress,
            Location = device.Location,
            Contact = device.Contact,
            Connection = DeviceConnectionDTO.FromDeviceConnection(device.DeviceConnection)
        };
    }
}