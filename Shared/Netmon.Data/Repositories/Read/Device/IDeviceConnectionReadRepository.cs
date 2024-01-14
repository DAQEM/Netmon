using Netmon.Data.DBO.Device;

namespace Netmon.Data.Repositories.Read.Device;

public interface IDeviceConnectionReadRepository : IReadRepository<DeviceConnectionDBO>
{
    public Task<DeviceConnectionDBO?> GetByDeviceId(Guid id);
}