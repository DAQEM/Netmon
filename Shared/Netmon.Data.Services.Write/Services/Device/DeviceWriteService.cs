using Netmon.Data.DBO.Device;
using Netmon.Data.Repositories.Write.Device;
using Netmon.Data.Services.Read.Device;
using Netmon.Data.Services.Write.Device;
using Netmon.Data.Services.Write.Exceptions;
using Netmon.Models.Device;
using Netmon.Models.Device.Connection;

namespace Netmon.Data.Services.Write.Services.Device;

public class DeviceWriteService : IDeviceWriteService
{
    private readonly IDeviceWriteRepository _deviceWriteRepository;
    private readonly IDeviceReadService _deviceReadService;
    private readonly IDeviceConnectionReadService _deviceConnectionReadService;

    public DeviceWriteService(IDeviceWriteRepository deviceWriteRepository, IDeviceReadService deviceReadService, IDeviceConnectionReadService deviceConnectionReadService)
    {
        _deviceWriteRepository = deviceWriteRepository;
        _deviceReadService = deviceReadService;
        _deviceConnectionReadService = deviceConnectionReadService;
    }

    public Task SaveChanges()
    {
        throw new NotImplementedException();
    }

    public async Task AddOrUpdateFullDevice(IDevice device)
    {
        IDevice? existingDevice = await _deviceReadService.GetByIpAddress(device.IpAddress);

        if (existingDevice == null) return;
        
        device.Id = existingDevice.Id;
        
        await _deviceWriteRepository.AddOrUpdateFullDevice(DeviceDBO.FromDevice(device));
        await _deviceWriteRepository.SaveChanges();
    }

    public Task AddOrUpdateDevice(IDevice device)
    {
        throw new NotImplementedException();
    }

    public async Task<IDevice> AddDeviceWithConnection(IDevice device)
    {
        IDevice? existingDevice = await _deviceReadService.GetByIpAddress(device.IpAddress);
        if (existingDevice != null) throw new DeviceWithIpAddressAlreadyExistsException(device.IpAddress);
        
        device = await _deviceWriteRepository.AddDeviceWithConnection(DeviceDBO.FromDevice(device));
        await _deviceWriteRepository.SaveChanges();
        
        return device;
    }

    public async Task UpdateWithConnection(IDevice device)
    {
        await _deviceWriteRepository.UpdateWithConnection(DeviceDBO.FromDevice(device));
        await _deviceWriteRepository.SaveChanges();
    }

    public async Task Delete(Guid id)
    {
        await _deviceWriteRepository.Delete(id);
        await _deviceWriteRepository.SaveChanges();
    }
}