using Microsoft.EntityFrameworkCore;
using Netmon.Data.EntityFramework.Database;
using Netmon.Data.EntityFramework.DBO.Component.Cpu;
using Netmon.Data.EntityFramework.DBO.Component.Disk;
using Netmon.Data.EntityFramework.DBO.Component.Interface;
using Netmon.Data.EntityFramework.DBO.Component.Memory;
using Netmon.Data.EntityFramework.DBO.Device;
using Netmon.Data.Repositories.Write.Component.Cpu;
using Netmon.Data.Repositories.Write.Component.Disk;
using Netmon.Data.Repositories.Write.Component.Interface;
using Netmon.Data.Repositories.Write.Component.Memory;
using Netmon.Data.Repositories.Write.Device;
using Netmon.Models.Device;

namespace Netmon.Data.Write.Repositories.Device;

public class DeviceWriteRepository : IDeviceWriteRepository
{
    private readonly DevicesDatabase _database;
    
    private readonly IDeviceConnectionWriteRepository _deviceConnectionWriteRepository;
    private readonly ICpuWriteRepository _cpuWriteRepository;
    private readonly IDiskWriteRepository _diskWriteRepository;
    private readonly IInterfaceWriteRepository _interfaceWriteRepository;
    private readonly IMemoryWriteRepository _memoryWriteRepository;

    public DeviceWriteRepository(DevicesDatabase database, IDeviceConnectionWriteRepository deviceConnectionWriteRepository, ICpuWriteRepository cpuWriteRepository, IDiskWriteRepository diskWriteRepository, IInterfaceWriteRepository interfaceWriteRepository, IMemoryWriteRepository memoryWriteRepository)
    {
        _database = database;
        _deviceConnectionWriteRepository = deviceConnectionWriteRepository;
        _cpuWriteRepository = cpuWriteRepository;
        _diskWriteRepository = diskWriteRepository;
        _interfaceWriteRepository = interfaceWriteRepository;
        _memoryWriteRepository = memoryWriteRepository;
    }

    public async Task AddOrUpdateFullDevice(IDevice device)
    {
        if (device == null) 
        {
            throw new ArgumentNullException(nameof(device));
        }

        await AddOrUpdateDevice(device);
        
        DeviceDBO deviceDBO = DeviceDBO.FromDevice(device);

        if (deviceDBO.DeviceConnection != null!)
        {
            deviceDBO.DeviceConnection.DeviceId = device.Id;
            await _deviceConnectionWriteRepository.AddOrUpdate(deviceDBO.DeviceConnection.ToDeviceConnection());
        }

        if (deviceDBO.Cpus != null!)
        {
            foreach (CpuDBO cpu in deviceDBO.Cpus)
            {
                cpu.DeviceId = device.Id;
                await _cpuWriteRepository.AddOrUpdate(cpu.ToCpu());
            }
        }
        
        if (deviceDBO.Disks != null!)
        {
            foreach (DiskDBO disk in deviceDBO.Disks)
            {
                disk.DeviceId = deviceDBO.Id;
                await _diskWriteRepository.AddOrUpdate(disk.ToDisk());
            }
        }
        
        if (deviceDBO.Interfaces != null!)
        {
            foreach (InterfaceDBO @interface in deviceDBO.Interfaces)
            {
                @interface.DeviceId = deviceDBO.Id;
                await _interfaceWriteRepository.AddOrUpdate(@interface.ToInterface());
            }
        }

        if (deviceDBO.Memory != null!)
        {
            foreach (MemoryDBO memory in deviceDBO.Memory)
            {
                memory.DeviceId = deviceDBO.Id;
                await _memoryWriteRepository.AddOrUpdate(memory.ToMemory());
            }
        }
    }

    public async Task AddOrUpdateDevice(IDevice device)
    {
        if(device == null)
        {
            throw new ArgumentNullException(nameof(device));
        }
        
        DeviceDBO deviceDBO = DeviceDBO.FromDevice(device);
        
        DeviceDBO? existingDevice = await _database.Devices.FirstOrDefaultAsync(d => d.IpAddress == deviceDBO.IpAddress);
        
        if (existingDevice != null)
        {
            deviceDBO.Id = existingDevice.Id;
            _database.Entry(existingDevice).CurrentValues.SetValues(deviceDBO);
        }
        else
        {
            deviceDBO.Id = Guid.NewGuid();
            await _database.Devices.AddAsync(deviceDBO);
        }
    }
    
    public async Task<IDevice> AddDeviceWithConnection(IDevice device)
    {
        if (device == null)
        {
            throw new ArgumentNullException(nameof(device));
        }
        
        DeviceDBO deviceDBO = DeviceDBO.FromDevice(device);
        
        if (device.DeviceConnection == null)
        {
            throw new ArgumentNullException(nameof(device.DeviceConnection));
        }
        
        await _database.Devices.AddAsync(deviceDBO);
        return deviceDBO.ToDevice();
    }

    public async Task UpdateWithConnection(IDevice device)
    {
        if (device == null)
        {
            throw new ArgumentNullException(nameof(device));
        }
        
        DeviceDBO deviceDBO = DeviceDBO.FromDevice(device);
        
        if (deviceDBO.DeviceConnection == null)
        {
            throw new ArgumentNullException(nameof(deviceDBO.DeviceConnection));
        }

        DeviceDBO existingDevice = await _database.Devices.FirstAsync(d => d.Id == deviceDBO.Id);
        
        existingDevice.IpAddress = deviceDBO.IpAddress;

        DeviceConnectionDBO existingDeviceConnection = await _database.DeviceConnections.FirstAsync(dc => dc.DeviceId == deviceDBO.Id);
        
        existingDeviceConnection.Port = deviceDBO.DeviceConnection.Port;
        existingDeviceConnection.Community = deviceDBO.DeviceConnection.Community;
        existingDeviceConnection.SNMPVersion = deviceDBO.DeviceConnection.SNMPVersion;
        existingDeviceConnection.AuthPassword = deviceDBO.DeviceConnection.AuthPassword;
        existingDeviceConnection.PrivacyPassword = deviceDBO.DeviceConnection.PrivacyPassword;
        existingDeviceConnection.AuthProtocol = deviceDBO.DeviceConnection.AuthProtocol;
        existingDeviceConnection.PrivacyProtocol = deviceDBO.DeviceConnection.PrivacyProtocol;
        existingDeviceConnection.ContextName = deviceDBO.DeviceConnection.ContextName;
    }

    public Task Delete(Guid id)
    {
        _database.Devices.Remove(new DeviceDBO { Id = id });
        return Task.CompletedTask;
    }

    public async Task SaveChanges()
    {
        await _database.SaveChangesAsync();
    }
}