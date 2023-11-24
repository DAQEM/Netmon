using Netmon.Models.Device.Connection;

namespace Netmon.Data.Services.Read.Device;

public interface IDeviceConnectionReadService : IReadService<IDeviceConnection>
{
    public Task<IDeviceConnection?> GetByDeviceId(Guid id);
}