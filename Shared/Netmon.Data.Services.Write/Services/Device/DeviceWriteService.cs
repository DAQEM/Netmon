using Netmon.Data.DBO.Device;
using Netmon.Data.Repositories.Write.Device;
using Netmon.Data.Services.Read.Device;
using Netmon.Data.Services.Write.Device;
using Netmon.Data.Services.Write.Exceptions;
using Netmon.Models.Device;

namespace Netmon.Data.Services.Write.Services.Device;

public class DeviceWriteService(
    IDeviceWriteRepository deviceWriteRepository,
    IDeviceReadService deviceReadService,
    IDeviceConnectionReadService deviceConnectionReadService)
    : IDeviceWriteService
{
    private readonly IDeviceConnectionReadService _deviceConnectionReadService = deviceConnectionReadService;

    public Task SaveChanges()
    {
        throw new NotImplementedException();
    }

    public async Task AddOrUpdateFullDevice(IDevice device)
    {
        IDevice? existingDevice = await deviceReadService.GetByIpAddress(device.IpAddress);

        if (existingDevice == null) return;
        
        device.Id = existingDevice.Id;
        
        await deviceWriteRepository.AddOrUpdateFullDevice(DeviceDBO.FromDevice(device));
        await deviceWriteRepository.SaveChanges();
    }

    public Task AddOrUpdateDevice(IDevice device)
    {
        throw new NotImplementedException();
    }

    public async Task<IDevice> AddDeviceWithConnection(IDevice device)
    {
        IDevice? existingDevice = await deviceReadService.GetByIpAddress(device.IpAddress);
        if (existingDevice != null) throw new DeviceWithIpAddressAlreadyExistsException(device.IpAddress);
        
        device = await deviceWriteRepository.AddDeviceWithConnection(DeviceDBO.FromDevice(device));
        await deviceWriteRepository.SaveChanges();
        
        return device;
    }

    public async Task UpdateWithConnection(IDevice device)
    {
        await deviceWriteRepository.UpdateWithConnection(DeviceDBO.FromDevice(device));
        await deviceWriteRepository.SaveChanges();
    }

    public async Task Delete(Guid id)
    {
        await deviceWriteRepository.Delete(id);
        await deviceWriteRepository.SaveChanges();
    }
}