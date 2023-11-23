using Netmon.Models.Exceptions;

namespace Netmon.Data.Services.Write.Exceptions;

public class DeviceWithIpAddressAlreadyExistsException : NetmonException
{
    public DeviceWithIpAddressAlreadyExistsException(string ipAddress) 
        : base($"Device with this IP address ({ipAddress}) already exists.")
    {
    }
}