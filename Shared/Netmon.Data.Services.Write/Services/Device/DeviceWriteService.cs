using Netmon.Data.DBO.Device;
using Netmon.Data.Repositories.Write.Device;
using Netmon.Data.Services.Read.Device;
using Netmon.Data.Services.Write.Device;
using Netmon.Data.Services.Write.Exceptions;

namespace Netmon.Data.Services.Write.Services.Device;

public class DeviceWriteService : IDeviceWriteService
{
    private readonly IDeviceWriteRepository _deviceWriteRepository;
    private readonly IDeviceReadService _deviceReadService;

    public DeviceWriteService(IDeviceWriteRepository deviceWriteRepository, IDeviceReadService deviceReadService)
    {
        _deviceWriteRepository = deviceWriteRepository;
        _deviceReadService = deviceReadService;
    }

    public Task SaveChanges()
    {
        throw new NotImplementedException();
    }

    public Task AddOrUpdateFullDevice(Models.Device.Device device)
    {
        throw new NotImplementedException();
    }

    public Task AddOrUpdateDevice(Models.Device.Device device)
    {
        throw new NotImplementedException();
    }

    public async Task<Models.Device.Device> AddDeviceWithConnection(Models.Device.Device device)
    {
        Models.Device.Device? existingDevice = await _deviceReadService.GetByIpAddress(device.IpAddress);
        if (existingDevice != null) throw new DeviceWithIpAddressAlreadyExistsException(device.IpAddress);
        
        DeviceDBO deviceDBO = DeviceDBO.FromDevice(device);
        deviceDBO = await _deviceWriteRepository.AddDeviceWithConnection(deviceDBO);
        await _deviceWriteRepository.SaveChanges();
        
        return deviceDBO.ToDevice();
    }

    public async Task UpdateWithConnection(Models.Device.Device device)
    {
        DeviceDBO deviceDBO = DeviceDBO.FromDevice(device);
        await _deviceWriteRepository.UpdateWithConnection(deviceDBO);
        await _deviceWriteRepository.SaveChanges();
    }

    public async Task Delete(Guid id)
    {
        await _deviceWriteRepository.Delete(id);
        await _deviceWriteRepository.SaveChanges();
    }
}