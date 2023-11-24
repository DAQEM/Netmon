using Microsoft.EntityFrameworkCore;
using Netmon.Data.EntityFramework.Database;
using Netmon.Data.EntityFramework.DBO.Device;
using Netmon.Data.Repositories.Write.Device;
using Netmon.Models.Device.Connection;

namespace Netmon.Data.Write.Repositories.Device;

public class DeviceConnectionWriteRepository : IDeviceConnectionWriteRepository
{
    private readonly DevicesDatabase _database;
    
    public DeviceConnectionWriteRepository(DevicesDatabase database)
    {
        _database = database;
    }
    
    public async Task AddOrUpdate(IDeviceConnection deviceConnection)
    {
        if (deviceConnection == null)
        {
            throw new ArgumentNullException(nameof(deviceConnection));
        }
        
        DeviceConnectionDBO deviceConnectionDBO = DeviceConnectionDBO.FromDeviceConnection(deviceConnection);
        
        DeviceConnectionDBO? existingDeviceConnection = await _database.DeviceConnections.FirstOrDefaultAsync(d => d.DeviceId == deviceConnectionDBO.DeviceId);
        
        if (existingDeviceConnection != null)
        {
            deviceConnectionDBO.Id = existingDeviceConnection.Id;
            _database.Entry(existingDeviceConnection).CurrentValues.SetValues(deviceConnectionDBO);
        }
        else
        {
            await _database.DeviceConnections.AddAsync(deviceConnectionDBO);
        }
    }
    
    public async Task SaveChanges()
    {
        await _database.SaveChangesAsync();
    }
}