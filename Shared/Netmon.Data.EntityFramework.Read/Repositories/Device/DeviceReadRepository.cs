using Microsoft.EntityFrameworkCore;
using Netmon.Data.DBO.Device;
using Netmon.Data.EntityFramework.Database;
using Netmon.Data.Repositories.Read.Component.Cpu;
using Netmon.Data.Repositories.Read.Component.Disk;
using Netmon.Data.Repositories.Read.Component.Interface;
using Netmon.Data.Repositories.Read.Component.Memory;
using Netmon.Data.Repositories.Read.Device;
using Netmon.Models.Device;

namespace Netmon.Data.EntityFramework.Read.Repositories.Device;

public class DeviceReadRepository : IDeviceReadRepository
{
    private readonly DevicesDatabase _database;

    public DeviceReadRepository(DevicesDatabase database)
    {
        _database = database;
    }

    public async Task<List<DeviceDBO>> GetAll()
    {
        return await _database.Devices.ToListAsync();
    }

    public async Task<DeviceDBO?> GetById(Guid id)
    {
        return await _database.Devices.FirstOrDefaultAsync(device => device.Id == id);
    }

    public async Task<DeviceDBO?> GetByIpAddress(string deviceDBOIpAddress)
    {
        return await _database.Devices.FirstOrDefaultAsync(device => device.IpAddress == deviceDBOIpAddress);
    }
}