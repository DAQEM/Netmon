using Netmon.Data.DBO.Device;
using Netmon.Data.Repositories.Read.Device;
using Netmon.Data.Services.Read.Device;
using Netmon.Models.Device.Connection;

namespace Netmon.Data.Services.Read.Services.Device;

public class DeviceReadService : IDeviceReadService
{
    private readonly IDeviceReadRepository _deviceReadRepository;
    private readonly IDeviceConnectionReadService _deviceConnectionReadService;
    
    public DeviceReadService(IDeviceReadRepository deviceReadRepository, IDeviceConnectionReadService deviceConnectionReadService)
    {
        _deviceReadRepository = deviceReadRepository;
        _deviceConnectionReadService = deviceConnectionReadService;
    }
    
    public async Task<List<Models.Device.Device>> GetAll()
    {
        List<DeviceDBO> deviceDBOs = await _deviceReadRepository.GetAll();
        return deviceDBOs.Select(dbo => dbo.ToDevice()).ToList();
    }

    public async Task<Models.Device.Device?> GetById(Guid id)
    {
        DeviceDBO? deviceDBO = await _deviceReadRepository.GetById(id);
        return deviceDBO?.ToDevice();
    }

    public async Task<Models.Device.Device?> GetById(Guid id, bool includeConnection)
    {
        Models.Device.Device? device = await GetById(id);
        if (device == null) return null;
        if (includeConnection)
        {
            DeviceConnection? deviceConnection = await _deviceConnectionReadService.GetByDeviceId(id);
            if (deviceConnection != null)
            {
                device.DeviceConnection = deviceConnection;
            }
        }
        return device;
    }

    public async Task<Models.Device.Device?> GetByIpAddress(string deviceDBOIpAddress)
    {
        DeviceDBO? deviceDBO = await _deviceReadRepository.GetByIpAddress(deviceDBOIpAddress);
        return deviceDBO?.ToDevice();
    }
}