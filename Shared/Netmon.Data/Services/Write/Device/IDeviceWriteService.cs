namespace Netmon.Data.Services.Write.Device;

public interface IDeviceWriteService : IWriteService
{
    Task AddOrUpdateFullDevice(Models.Device.Device device);
    Task AddOrUpdateDevice(Models.Device.Device device);
    Task<Models.Device.Device> AddDeviceWithConnection(Models.Device.Device device);
    Task UpdateWithConnection(Models.Device.Device device);
    Task Delete(Guid id);
}