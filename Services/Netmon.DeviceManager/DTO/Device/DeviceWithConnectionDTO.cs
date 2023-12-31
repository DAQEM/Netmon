﻿using System.Text.Json.Serialization;
using Netmon.Data.EntityFramework.Attribute.Validator;
using Netmon.Data.EntityFramework.DBO.Device;
using Netmon.Models.Device;

namespace Netmon.DeviceManager.DTO.Device;

public class DeviceWithConnectionDTO
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }
    [JsonPropertyName("name")]
    public string? Name { get; set; }
    [ValidateIpAddress]
    [JsonPropertyName("ip_address")]
    public string? IpAddress { get; set; }
    [JsonPropertyName("location")]
    public string? Location { get; set; }
    [JsonPropertyName("contact")]
    public string? Contact { get; set; }
    [JsonPropertyName("connection")]
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