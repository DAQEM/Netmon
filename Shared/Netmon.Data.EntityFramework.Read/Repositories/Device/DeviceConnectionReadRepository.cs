﻿using Microsoft.EntityFrameworkCore;
using Netmon.Data.DBO.Device;
using Netmon.Data.EntityFramework.Database;
using Netmon.Data.Repositories.Read.Device;

namespace Netmon.Data.EntityFramework.Read.Repositories.Device;

public class DeviceConnectionReadRepository : IDeviceConnectionReadRepository
{
    private readonly DevicesDatabase _database;
    
    public DeviceConnectionReadRepository(DevicesDatabase database)
    {
        _database = database;
    }

    public async Task<List<DeviceConnectionDBO>> GetAll()
    {
        return await _database.DeviceConnections.ToListAsync();
    }
}