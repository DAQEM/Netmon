using Microsoft.EntityFrameworkCore;
using Netmon.Data.EntityFramework.Database;
using Netmon.Data.EntityFramework.DBO.Device;
using Netmon.Data.Repositories.Read.Device;
using Netmon.Models.Device.Connection;

namespace Netmon.Data.EntityFramework.Read.Repositories.Device;

public class DeviceConnectionReadRepository : IDeviceConnectionReadRepository
{
    private readonly DevicesDatabase _database;
    
    public DeviceConnectionReadRepository(DevicesDatabase database)
    {
        _database = database;
    }

    public async Task<List<IDeviceConnection>> GetAll()
    {
        return await _database.DeviceConnections
            .Select(dbo => dbo.ToDeviceConnection())
            .ToListAsync();
    }

    public async Task<IDeviceConnection?> GetById(Guid id)
    {
        return (await _database.DeviceConnections
                .FirstOrDefaultAsync(device => device.Id == id))?.
            ToDeviceConnection();
    }

    public async Task<IDeviceConnection?> GetByDeviceId(Guid id)
    {
        return (await _database.DeviceConnections
                .FirstOrDefaultAsync(device => device.DeviceId == id))?
            .ToDeviceConnection();
    }
}