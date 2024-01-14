using Netmon.Models.Exceptions;

namespace Netmon.Data.Services.Write.Exceptions;

public class DeviceWithIpAddressAlreadyExistsException(string ipAddress)
    : NetmonException($"Device with this IP address ({ipAddress}) already exists.");