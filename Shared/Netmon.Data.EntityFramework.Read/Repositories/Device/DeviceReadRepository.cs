using Microsoft.EntityFrameworkCore;
using Netmon.Data.DBO.Component.Cpu;
using Netmon.Data.DBO.Component.Disk;
using Netmon.Data.DBO.Component.Interface;
using Netmon.Data.DBO.Component.Memory;
using Netmon.Data.DBO.Device;
using Netmon.Data.EntityFramework.Database;
using Netmon.Data.Repositories.Read.Component.Cpu;
using Netmon.Data.Repositories.Read.Component.Disk;
using Netmon.Data.Repositories.Read.Component.Interface;
using Netmon.Data.Repositories.Read.Component.Memory;
using Netmon.Data.Repositories.Read.Device;

namespace Netmon.Data.EntityFramework.Read.Repositories.Device;

public class DeviceReadRepository : IDeviceReadRepository
{
    private readonly DevicesDatabase _database;
    
    private readonly IDeviceConnectionReadRepository _deviceConnectionReadRepository;
    private readonly ICpuReadRepository _cpuReadRepository;
    private readonly IDiskReadRepository _diskReadRepository;
    private readonly IInterfaceReadRepository _interfaceReadRepository;
    private readonly IMemoryReadRepository _memoryReadRepository;

    public DeviceReadRepository(DevicesDatabase database, IDeviceConnectionReadRepository deviceConnectionReadRepository, ICpuReadRepository cpuReadRepository, IDiskReadRepository diskReadRepository, IInterfaceReadRepository interfaceReadRepository, IMemoryReadRepository memoryReadRepository)
    {
        _database = database;
        _deviceConnectionReadRepository = deviceConnectionReadRepository;
        _cpuReadRepository = cpuReadRepository;
        _diskReadRepository = diskReadRepository;
        _interfaceReadRepository = interfaceReadRepository;
        _memoryReadRepository = memoryReadRepository;
    }

    public async Task<List<DeviceDBO>> GetAll()
    {
        return await _database.Devices.ToListAsync();
    }
}