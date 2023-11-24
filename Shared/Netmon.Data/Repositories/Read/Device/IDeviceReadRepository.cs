using Netmon.Models.Device;

namespace Netmon.Data.Repositories.Read.Device;

public interface IDeviceReadRepository : IReadRepository<IDevice>
{
    Task<IDevice?> GetByIpAddress(string deviceDBOIpAddress);
}