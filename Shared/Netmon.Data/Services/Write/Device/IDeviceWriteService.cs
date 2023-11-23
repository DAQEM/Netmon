using Netmon.Data.DBO.Device;
using Netmon.Data.Repositories.Write;

namespace Netmon.Data.Services.Write.Device;

public interface IDeviceWriteService : IWriteService
{
    Task AddOrUpdateFullDevice(Models.Device.Device device);
    Task AddOrUpdateDevice(Models.Device.Device device);
    Task<Models.Device.Device> AddDeviceWithConnection(Models.Device.Device toDevice);
    Task UpdateWithConnection(Models.Device.Device deviceDBO);
    Task Delete(Models.Device.Device deviceDBO);
}