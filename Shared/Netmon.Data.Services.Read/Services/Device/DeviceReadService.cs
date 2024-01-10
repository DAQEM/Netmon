using Netmon.Data.DBO.Device;
using Netmon.Data.Repositories.Read.Device;
using Netmon.Data.Services.Read.Device;
using Netmon.Models.Device;
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
    
    public async Task<List<IDevice>> GetAll()
    {
        List<DeviceDBO> deviceDbOs = await _deviceReadRepository.GetAll();
        return deviceDbOs?.Select(x => x.ToDevice()).ToList() ?? new List<IDevice>();
    }

    public async Task<IDevice?> GetById(Guid id)
    {
        DeviceDBO? deviceDBO = await _deviceReadRepository.GetById(id);
        return deviceDBO?.ToDevice();
    }

    public async Task<IDevice?> GetById(Guid id, bool includeConnection)
    {
        IDevice? device = await GetById(id);
        if (device == null) return null;
        if (includeConnection)
        {
            IDeviceConnection? deviceConnection = await _deviceConnectionReadService.GetByDeviceId(id);
            if (deviceConnection != null)
            {
                device.DeviceConnection = deviceConnection;
            }
        }
        return device;
    }

    public async Task<IDevice?> GetByIpAddress(string deviceDBOIpAddress)
    {
        DeviceDBO? deviceDBO = await _deviceReadRepository.GetByIpAddress(deviceDBOIpAddress);
        return deviceDBO?.ToDevice();
    }
}