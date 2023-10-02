using DevicesLib.DBO.Device;

namespace DevicesLib.Repositories.Device;

public interface IDeviceRepository
{
    Task AddOrUpdateDevice(DeviceDBO device);
    
    Task AddOrUpdateFullDevice(DeviceDBO device);
    
    Task SaveChanges();
}