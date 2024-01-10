using Netmon.Data.DBO.Device;
using Netmon.Models.Device;

namespace Netmon.Data.Repositories.Read.Device;

public interface IDeviceReadRepository : IReadRepository<DeviceDBO>
{
    Task<DeviceDBO?> GetByIpAddress(string deviceDBOIpAddress);
}