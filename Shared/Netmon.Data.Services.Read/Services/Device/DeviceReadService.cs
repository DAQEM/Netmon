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
        List<IDevice> deviceDBOs = await _deviceReadRepository.GetAll();
        return deviceDBOs;
    }

    public async Task<IDevice?> GetById(Guid id)
    {
        IDevice? deviceDBO = await _deviceReadRepository.GetById(id);
        return deviceDBO;
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
        IDevice? deviceDBO = await _deviceReadRepository.GetByIpAddress(deviceDBOIpAddress);
        return deviceDBO;
    }
}