using Netmon.Data.Repositories.Read.Device;
using Netmon.Data.Services.Read.Device;
using Netmon.Models.Device.Connection;

namespace Netmon.Data.Services.Read.Services.Device;

public class DeviceConnectionReadService(IDeviceConnectionReadRepository deviceConnectionReadRepository)
    : IDeviceConnectionReadService
{
    public async Task<List<IDeviceConnection>> GetAll()
    {
        return (await deviceConnectionReadRepository.GetAll())
            .Select(x => x.ToDeviceConnection())
            .ToList();
    }

    public Task<IDeviceConnection?> GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<IDeviceConnection?> GetByDeviceId(Guid id)
    {
        return (await deviceConnectionReadRepository.GetByDeviceId(id))?.ToDeviceConnection();
    }
}