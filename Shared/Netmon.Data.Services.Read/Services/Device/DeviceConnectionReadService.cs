using Netmon.Data.DBO.Device;
using Netmon.Data.Services.Read.Device;
using Netmon.Models.Device.Connection;

namespace Netmon.Data.Services.Read.Services.Device;

public class DeviceConnectionReadService : IDeviceConnectionReadService
{
    public async Task<List<DeviceConnection>> GetAll()
    {
        throw new NotImplementedException();
    }

    public async Task<DeviceConnection?> GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<DeviceConnection?> GetByDeviceId(Guid id)
    {
        throw new NotImplementedException();
    }
}