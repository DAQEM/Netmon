using DevicesLib.Database;
using DevicesLib.DBO.Device;
using Microsoft.EntityFrameworkCore;

namespace DevicesLib.Repositories.Device;

public class DeviceConnectionRepository : IDeviceConnectionRepository
{
    private readonly DevicesDatabase _database;
    
    public DeviceConnectionRepository(DevicesDatabase database)
    {
        _database = database;
    }
    
    public async Task AddOrUpdateDeviceConnection(DeviceConnectionDBO deviceConnection)
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