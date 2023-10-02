using DevicesLib.DBO.Device;

namespace DevicesLib.Repositories.Device;

public interface IDeviceRepository
{
    Task AddOrUpdateDevice(DeviceDBO device);
    
    Task AddOrUpdateDeviceConnection(DeviceConnectionDBO deviceConnection);
    
    Task AddOrUpdateFullDevice(DeviceDBO device);
}