using Microsoft.EntityFrameworkCore;
using Netmon.Data.DBO.Device;
using Netmon.Data.EntityFramework.Database;
using Netmon.Data.Repositories.Write.Device;

namespace Netmon.Data.Write.Repositories.Device;

public class DeviceConnectionWriteRepository(DevicesDatabase database) : IDeviceConnectionWriteRepository
{
    public async Task AddOrUpdate(DeviceConnectionDBO deviceConnection)
    {
        if (deviceConnection is null)
        {
            throw new ArgumentNullException(nameof(deviceConnection));
        }
        
        DeviceConnectionDBO? existingDeviceConnection = await database.DeviceConnections.FirstOrDefaultAsync(d => d.DeviceId == deviceConnection.DeviceId);
        
        if (existingDeviceConnection is not null)
        {
            deviceConnection.Id = existingDeviceConnection.Id;
            database.Entry(existingDeviceConnection).CurrentValues.SetValues(deviceConnection);
        }
        else
        {
            await database.DeviceConnections.AddAsync(deviceConnection);
        }
    }
    
    public async Task SaveChanges()
    {
        await database.SaveChangesAsync();
    }
}