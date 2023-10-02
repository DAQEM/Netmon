using System.Diagnostics.SymbolStore;
using DevicesLib.Database;
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
    
    private readonly ICpuRepository _cpuRepository;
    private readonly IDiskRepository _diskRepository;
    private readonly IInterfaceRepository _interfaceRepository;
    private readonly IMemoryRepository _memoryRepository;

    public DeviceRepository(DevicesDatabase database, ICpuRepository cpuRepository, IDiskRepository diskRepository, IInterfaceRepository interfaceRepository, IMemoryRepository memoryRepository)
    {
        _database = database;
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
            await AddOrUpdateDeviceConnection(device.DeviceConnection);
        }

        if (device.Cpus != null!)
        {
            device.Cpus.ForEach(async cpu => await _cpuRepository.AddOrUpdateCpu(cpu));
        }
        
        if (device.Disks != null!)
        {
            device.Disks.ForEach(async disk => await _diskRepository.AddOrUpdateDisk(disk));
        }
        
        if (device.Interfaces != null!)
        {
            device.Interfaces.ForEach(async @interface => await _interfaceRepository.AddOrUpdateInterface(@interface));
        }
        
        if (device.Memory != null!)
        {
            device.Memory.ForEach(async memory => await _memoryRepository.AddOrUpdateMemory(memory));
        }
    }

    public async Task AddOrUpdateDevice(DeviceDBO device)
    {
        if(device == null)
        {
            throw new ArgumentNullException(nameof(device));
        }
        
        DeviceDBO? existingDevice = await _database.Devices.FirstOrDefaultAsync(d => d.Id == device.Id);
        
        if (existingDevice != null)
        {
            _database.Entry(existingDevice).CurrentValues.SetValues(device);
        }
        else
        {
            await _database.Devices.AddAsync(device);
        }

        await _database.SaveChangesAsync();
    }

    public async Task AddOrUpdateDeviceConnection(DeviceConnectionDBO deviceConnection)
    {
        if (deviceConnection == null)
        {
            throw new ArgumentNullException(nameof(deviceConnection));
        }
        
        DeviceConnectionDBO? existingDeviceConnection = await _database.DeviceConnections.FirstOrDefaultAsync(d => d.Id == deviceConnection.Id);
        
        if (existingDeviceConnection != null)
        {
            _database.Entry(existingDeviceConnection).CurrentValues.SetValues(deviceConnection);
        }
        else
        {
            await _database.DeviceConnections.AddAsync(deviceConnection);
        }
        
        await _database.SaveChangesAsync();
    }
}