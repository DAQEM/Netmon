using Netmon.Data.DBO.Device;

namespace Netmon.Data.Repositories.Read.Device;

public interface IDeviceReadRepository : IReadRepository<DeviceDBO>
{
    Task<DeviceDBO?> GetByIpAddress(string deviceDBOIpAddress);
}