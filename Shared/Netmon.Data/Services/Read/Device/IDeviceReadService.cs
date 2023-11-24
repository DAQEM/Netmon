using Netmon.Models.Device;

namespace Netmon.Data.Services.Read.Device;

public interface IDeviceReadService : IReadService<IDevice>
{
    Task<IDevice?> GetById(Guid id, bool includeConnection);
    Task<IDevice?> GetByIpAddress(string deviceDBOIpAddress);
}