using Microsoft.EntityFrameworkCore;
using Netmon.Data.DBO.Device;
using Netmon.Data.EntityFramework.Database;
using Netmon.Data.Repositories.Read.Device;

namespace Netmon.Data.EntityFramework.Read.Repositories.Device;

public class DeviceReadRepository(DevicesDatabase database) : IDeviceReadRepository
{
    public async Task<List<DeviceDBO>> GetAll()
    {
        return await database.Devices.ToListAsync();
    }

    public async Task<DeviceDBO?> GetById(Guid id)
    {
        return await database.Devices.FirstOrDefaultAsync(device => device.Id == id);
    }

    public async Task<DeviceDBO?> GetByIpAddress(string deviceDBOIpAddress)
    {
        return await database.Devices.FirstOrDefaultAsync(device => device.IpAddress == deviceDBOIpAddress);
    }
}