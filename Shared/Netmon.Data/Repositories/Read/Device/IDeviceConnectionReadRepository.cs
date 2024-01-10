using Netmon.Data.DBO.Device;
using Netmon.Models.Device.Connection;

namespace Netmon.Data.Repositories.Read.Device;

public interface IDeviceConnectionReadRepository : IReadRepository<DeviceConnectionDBO>
{
    public Task<DeviceConnectionDBO?> GetByDeviceId(Guid id);
}