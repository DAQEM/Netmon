using Netmon.Models.Device;

namespace Netmon.Data.Services.Write.Device;

public interface IDeviceWriteService : IWriteService
{
    Task AddOrUpdateFullDevice(IDevice device);
    Task AddOrUpdateDevice(IDevice device);
    Task<IDevice> AddDeviceWithConnection(IDevice device);
    Task UpdateWithConnection(IDevice device);
    Task Delete(Guid id);
}