namespace Netmon.Data.Services.Read.Device;

public interface IDeviceReadService : IReadService<Models.Device.Device>
{
    Task<Models.Device.Device?> GetByIpAddress(string deviceDBOIpAddress);
}