using DevicesLib.DBO.Device;

namespace DevicesLib.Repositories.Device;

public interface IDeviceConnectionRepository
{
    Task AddOrUpdateDeviceConnection(DeviceConnectionDBO deviceConnection);
    
    Task SaveChanges();
}