using Netmon.Data.Services.Write.Device;

namespace Netmon.Data.Services.Write.Services.Device;

public class DeviceWriteService : IDeviceWriteService
{
    public Task SaveChanges()
    {
        throw new NotImplementedException();
    }

    public Task AddOrUpdateFullDevice(Models.Device.Device device)
    {
        throw new NotImplementedException();
    }

    public Task AddOrUpdateDevice(Models.Device.Device device)
    {
        throw new NotImplementedException();
    }

    public Task<Models.Device.Device> AddDeviceWithConnection(Models.Device.Device toDevice)
    {
        throw new NotImplementedException();
    }

    public Task UpdateWithConnection(Models.Device.Device deviceDBO)
    {
        throw new NotImplementedException();
    }

    public Task Delete(Models.Device.Device deviceDBO)
    {
        throw new NotImplementedException();
    }
}