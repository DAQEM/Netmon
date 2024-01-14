using Microsoft.EntityFrameworkCore;
using Netmon.Data.DBO.Device;
using Netmon.Data.EntityFramework.Database;
using Netmon.Data.Repositories.Read.Device;

namespace Netmon.Data.EntityFramework.Read.Repositories.Device;

public class DeviceConnectionReadRepository(DevicesDatabase database) : IDeviceConnectionReadRepository
{
    public async Task<List<DeviceConnectionDBO>> GetAll()
    {
        return await database.DeviceConnections
            .ToListAsync();
    }

    public async Task<DeviceConnectionDBO?> GetById(Guid id)
    {
        return await database.DeviceConnections
                .FirstOrDefaultAsync(device => device.Id == id);
    }

    public async Task<DeviceConnectionDBO?> GetByDeviceId(Guid id)
    {
        return await database.DeviceConnections
                .FirstOrDefaultAsync(device => device.DeviceId == id);
    }
}