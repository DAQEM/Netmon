using Netmon.Data.DBO.Device;
using Netmon.Data.Repositories.Read;
using Netmon.Models.Device.Connection;

namespace Netmon.Data.Services.Read.Device;

public interface IDeviceConnectionReadService : IReadService<DeviceConnection>
{
    public Task<DeviceConnection?> GetByDeviceId(Guid id);
}