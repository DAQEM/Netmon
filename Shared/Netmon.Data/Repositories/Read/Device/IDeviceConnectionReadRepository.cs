using Netmon.Models.Device.Connection;

namespace Netmon.Data.Repositories.Read.Device;

public interface IDeviceConnectionReadRepository : IReadRepository<IDeviceConnection>
{
    public Task<IDeviceConnection?> GetByDeviceId(Guid id);
}