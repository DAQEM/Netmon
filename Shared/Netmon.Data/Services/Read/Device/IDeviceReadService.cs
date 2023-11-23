namespace Netmon.Data.Services.Read.Device;

public interface IDeviceReadService : IReadService<Models.Device.Device>
{
    Task<Netmon.Models.Device.Device?> GetById(Guid id, bool includeConnection);
    Task<Models.Device.Device?> GetByIpAddress(string deviceDBOIpAddress);
}