using Netmon.Data.DBO.Device;

namespace Netmon.Data.Repositories.Write.Device;

public interface IDeviceWriteRepository : IWriteRepository
{
    Task AddOrUpdateFullDevice(DeviceDBO device);
}