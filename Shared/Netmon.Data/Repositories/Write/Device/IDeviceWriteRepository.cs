using Netmon.Models.Device;

namespace Netmon.Data.Repositories.Write.Device;

public interface IDeviceWriteRepository : IWriteRepository
{
    Task AddOrUpdateFullDevice(IDevice device);
    Task AddOrUpdateDevice(IDevice device);
    Task<IDevice> AddDeviceWithConnection(IDevice device);
    Task UpdateWithConnection(IDevice device);
    Task Delete(Guid id);
}