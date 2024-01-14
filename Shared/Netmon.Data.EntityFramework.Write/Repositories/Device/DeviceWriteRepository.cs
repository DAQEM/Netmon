using Microsoft.EntityFrameworkCore;
using Netmon.Data.DBO.Component.Cpu;
using Netmon.Data.DBO.Component.Disk;
using Netmon.Data.DBO.Component.Interface;
using Netmon.Data.DBO.Component.Memory;
using Netmon.Data.DBO.Device;
using Netmon.Data.EntityFramework.Database;
using Netmon.Data.Repositories.Write.Component.Cpu;
using Netmon.Data.Repositories.Write.Component.Disk;
using Netmon.Data.Repositories.Write.Component.Interface;
using Netmon.Data.Repositories.Write.Component.Memory;
using Netmon.Data.Repositories.Write.Device;
using Netmon.Models.Device;

namespace Netmon.Data.Write.Repositories.Device;

public class DeviceWriteRepository(
    DevicesDatabase database,
    IDeviceConnectionWriteRepository deviceConnectionWriteRepository,
    ICpuWriteRepository cpuWriteRepository,
    IDiskWriteRepository diskWriteRepository,
    IInterfaceWriteRepository interfaceWriteRepository,
    IMemoryWriteRepository memoryWriteRepository)
    : IDeviceWriteRepository
{
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
            await deviceConnectionWriteRepository.AddOrUpdate(device.DeviceConnection);
        }

        if (device.Cpus != null!)
        {
            foreach (CpuDBO cpu in device.Cpus)
            {
                cpu.DeviceId = device.Id;
                await cpuWriteRepository.AddOrUpdate(cpu);
            }
        }
        
        if (device.Disks != null!)
        {
            foreach (DiskDBO disk in device.Disks)
            {
                disk.DeviceId = device.Id;
                await diskWriteRepository.AddOrUpdate(disk);
            }
        }
        
        if (device.Interfaces != null!)
        {
            foreach (InterfaceDBO @interface in device.Interfaces)
            {
                @interface.DeviceId = device.Id;
                await interfaceWriteRepository.AddOrUpdate(@interface);
            }
        }

        if (device.Memory != null!)
        {
            foreach (MemoryDBO memory in device.Memory)
            {
                memory.DeviceId = device.Id;
                await memoryWriteRepository.AddOrUpdate(memory);
            }
        }
    }

    public async Task AddOrUpdateDevice(DeviceDBO device)
    {
        if(device == null)
        {
            throw new ArgumentNullException(nameof(device));
        }
        
        DeviceDBO? existingDevice = await database.Devices.FirstOrDefaultAsync(d => d.IpAddress == device.IpAddress);
        
        if (existingDevice != null)
        {
            device.Id = existingDevice.Id;
            database.Entry(existingDevice).CurrentValues.SetValues(device);
        }
        else
        {
            device.Id = Guid.NewGuid();
            await database.Devices.AddAsync(device);
        }
    }
    
    public async Task<IDevice> AddDeviceWithConnection(DeviceDBO device)
    {
        if (device == null)
        {
            throw new ArgumentNullException(nameof(device));
        }
        
        if (device.DeviceConnection == null)
        {
            throw new ArgumentNullException(nameof(device.DeviceConnection));
        }
        
        await database.Devices.AddAsync(device);
        return device.ToDevice();
    }

    public async Task UpdateWithConnection(DeviceDBO device)
    {
        if (device == null)
        {
            throw new ArgumentNullException(nameof(device));
        }
        
        if (device.DeviceConnection == null)
        {
            throw new ArgumentNullException(nameof(device.DeviceConnection));
        }

        DeviceDBO existingDevice = await database.Devices.FirstAsync(d => d.Id == device.Id);
        
        existingDevice.IpAddress = device.IpAddress;

        DeviceConnectionDBO existingDeviceConnection = await database.DeviceConnections.FirstAsync(dc => dc.DeviceId == device.Id);
        
        existingDeviceConnection.Port = device.DeviceConnection.Port;
        existingDeviceConnection.Community = device.DeviceConnection.Community;
        existingDeviceConnection.SNMPVersion = device.DeviceConnection.SNMPVersion;
        existingDeviceConnection.AuthPassword = device.DeviceConnection.AuthPassword;
        existingDeviceConnection.PrivacyPassword = device.DeviceConnection.PrivacyPassword;
        existingDeviceConnection.AuthProtocol = device.DeviceConnection.AuthProtocol;
        existingDeviceConnection.PrivacyProtocol = device.DeviceConnection.PrivacyProtocol;
        existingDeviceConnection.ContextName = device.DeviceConnection.ContextName;
    }

    public Task Delete(Guid id)
    {
        database.Devices.Remove(new DeviceDBO { Id = id });
        return Task.CompletedTask;
    }

    public async Task SaveChanges()
    {
        await database.SaveChangesAsync();
    }
}