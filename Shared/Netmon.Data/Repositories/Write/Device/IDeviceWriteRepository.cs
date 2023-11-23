using Netmon.Data.DBO.Device;

namespace Netmon.Data.Repositories.Write.Device;

public interface IDeviceWriteRepository : IWriteRepository
{
    Task AddOrUpdateFullDevice(DeviceDBO device);
    Task AddOrUpdateDevice(DeviceDBO device);
    Task<DeviceDBO> AddDeviceWithConnection(DeviceDBO toDevice);
    Task UpdateWithConnection(DeviceDBO deviceDBO);
    Task Delete(Guid id);
}