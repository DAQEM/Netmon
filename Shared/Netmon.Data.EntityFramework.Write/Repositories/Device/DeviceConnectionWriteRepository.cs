using Microsoft.EntityFrameworkCore;
using Netmon.Data.DBO.Device;
using Netmon.Data.EntityFramework.Database;
using Netmon.Data.Repositories.Write.Device;

namespace Netmon.Data.Write.Repositories.Device;

public class DeviceConnectionWriteRepository : IDeviceConnectionWriteRepository
{
    private readonly DevicesDatabase _database;
    
    public DeviceConnectionWriteRepository(DevicesDatabase database)
    {
        _database = database;
    }
    
    public async Task AddOrUpdate(DeviceConnectionDBO deviceConnection)
    {
        if (deviceConnection == null)
        {
            throw new ArgumentNullException(nameof(deviceConnection));
        }
        
        DeviceConnectionDBO? existingDeviceConnection = await _database.DeviceConnections.FirstOrDefaultAsync(d => d.DeviceId == deviceConnection.DeviceId);
        
        if (existingDeviceConnection != null)
        {
            deviceConnection.Id = existingDeviceConnection.Id;
            _database.Entry(existingDeviceConnection).CurrentValues.SetValues(deviceConnection);
        }
        else
        {
            await _database.DeviceConnections.AddAsync(deviceConnection);
        }
    }
    
    public async Task SaveChanges()
    {
        await _database.SaveChangesAsync();
    }
}