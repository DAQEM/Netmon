using Netmon.Data.DBO.Device;
using Netmon.Models.Device;

namespace Netmon.Data.Repositories.Write.Device;

public interface IDeviceWriteRepository : IWriteRepository
{
    Task AddOrUpdateFullDevice(DeviceDBO device);
    Task AddOrUpdateDevice(DeviceDBO device);
    Task<DeviceDBO> AddDeviceWithConnection(DeviceDBO toDevice);
    Task UpdateWithConnection(DeviceDBO deviceDBO);
}