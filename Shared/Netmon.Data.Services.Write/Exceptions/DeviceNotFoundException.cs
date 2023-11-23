using Netmon.Models.Exceptions;

namespace Netmon.Data.Services.Write.Exceptions;

public class DeviceNotFoundException : NetmonException
{
    public DeviceNotFoundException(string deviceId)
        : base($"Device with ID ({deviceId}) not found.")
    {
    }
    
    public DeviceNotFoundException(Guid deviceId)
        : base($"Device with ID ({deviceId.ToString()}) not found.")
    {
    }
}