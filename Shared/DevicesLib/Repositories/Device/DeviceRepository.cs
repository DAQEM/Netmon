using System.Diagnostics.SymbolStore;
using DevicesLib.Database;
using DevicesLib.DBO.Component.Cpu;
using DevicesLib.DBO.Component.Disk;
using DevicesLib.DBO.Component.Interface;
using DevicesLib.DBO.Component.Memory;
using DevicesLib.DBO.Device;
using DevicesLib.Repositories.Component.Cpu;
using DevicesLib.Repositories.Component.Disk;
using DevicesLib.Repositories.Component.Interface;
using DevicesLib.Repositories.Component.Memory;
using Microsoft.EntityFrameworkCore;

namespace DevicesLib.Repositories.Device;

public class DeviceRepository : IDeviceRepository
{
    private readonly DevicesDatabase _database;
    
    private readonly IDeviceConnectionRepository _deviceConnectionRepository;
    private readonly ICpuRepository _cpuRepository;
    private readonly IDiskRepository _diskRepository;
    private readonly IInterfaceRepository _interfaceRepository;
    private readonly IMemoryRepository _memoryRepository;

    public DeviceRepository(DevicesDatabase database, IDeviceConnectionRepository deviceConnectionRepository, ICpuRepository cpuRepository, IDiskRepository diskRepository, IInterfaceRepository interfaceRepository, IMemoryRepository memoryRepository)
    {
        _database = database;
        _deviceConnectionRepository = deviceConnectionRepository;
        _cpuRepository = cpuRepository;
        _diskRepository = diskRepository;
        _interfaceRepository = interfaceRepository;
        _memoryRepository = memoryRepository;
    }

    public async Task AddOrUpdateFullDevice(DeviceDBO device)
    {
        if (device == null) 
        {
            throw new ArgumentNullException(nameof(device));
        }
        
        await AddOrUpdateDevice(device);
        if (device.DeviceConnection != null!)
        {
            device.DeviceConnection.DeviceId = device.Id;
            await _deviceConnectionRepository.AddOrUpdateDeviceConnection(device.DeviceConnection);
        }

        if (device.Cpus != null!)
        {
            foreach (CpuDBO cpu in device.Cpus)
            {
                cpu.DeviceId = device.Id;
                await _cpuRepository.AddOrUpdateCpu(cpu);
            }
        }
        
        if (device.Disks != null!)
        {
            foreach (DiskDBO disk in device.Disks)
            {
                disk.DeviceId = device.Id;
                await _diskRepository.AddOrUpdateDisk(disk);
            }
        }
        
        if (device.Interfaces != null!)
        {
            foreach (InterfaceDBO @interface in device.Interfaces)
            {
                @interface.DeviceId = device.Id;
                await _interfaceRepository.AddOrUpdateInterface(@interface);
            }
        }

        if (device.Memory != null!)
        {
            foreach (MemoryDBO memory in device.Memory)
            {
                memory.DeviceId = device.Id;
                await _memoryRepository.AddOrUpdateMemory(memory);
            }
        }
    }

    public async Task AddOrUpdateDevice(DeviceDBO device)
    {
        if(device == null)
        {
            throw new ArgumentNullException(nameof(device));
        }
        
        DeviceDBO? existingDevice = await _database.Devices.FirstOrDefaultAsync(d => d.IpAddress == device.IpAddress);
        
        if (existingDevice != null)
        {
            device.Id = existingDevice.Id;
            _database.Entry(existingDevice).CurrentValues.SetValues(device);
        }
        else
        {
            device.Id = Guid.NewGuid();
            await _database.Devices.AddAsync(device);
        }
    }
    
    public async Task SaveChanges()
    {
        await _database.SaveChangesAsync();
    }
}