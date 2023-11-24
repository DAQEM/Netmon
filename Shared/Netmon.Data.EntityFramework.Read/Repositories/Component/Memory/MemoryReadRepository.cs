using Microsoft.EntityFrameworkCore;
using Netmon.Data.EntityFramework.Database;
using Netmon.Data.EntityFramework.DBO.Component.Memory;
using Netmon.Data.Repositories.Read.Component.Memory;
using Netmon.Models.Component.Memory;

namespace Netmon.Data.EntityFramework.Read.Repositories.Component.Memory;

public class MemoryReadRepository : IMemoryReadRepository
{
    private readonly DevicesDatabase _database;

    public MemoryReadRepository(DevicesDatabase database)
    {
        _database = database;
    }

    public async Task<List<IMemory>> GetAll()
    {
        return await _database.Memory
            .Select(dbo => dbo.ToMemory())
            .ToListAsync();
    }

    public async Task<IMemory?> GetById(Guid id)
    {
        return (await _database.Memory
            .FirstOrDefaultAsync(device => device.Id == id))?
            .ToMemory();
    }
    
    public async Task<List<IMemory>> GetByDeviceId(Guid deviceId)
    {
        return await _database.Memory
            .Where(memory => memory.DeviceId == deviceId)
            .Select(dbo => dbo.ToMemory())
            .ToListAsync();
    }

    public async Task<List<IMemory>> GetByDeviceIdWithMetrics(Guid deviceId)
    {
        return await _database.Memory
            .Include(memory => memory.MemoryMetrics)
            .Where(memory => memory.DeviceId == deviceId)
            .Select(dbo => dbo.ToMemory())
            .ToListAsync();
    }
    
    public async Task<List<IMemory>> GetByDeviceIdWithMetrics(Guid deviceId, DateTime from, DateTime to)
    {
        return await _database.Memory
            .Include(memory => memory.MemoryMetrics)
            .Where(memory => memory.DeviceId == deviceId)
            .Where(memory => memory.MemoryMetrics.Any(metric => metric.Timestamp >= from && metric.Timestamp <= to))
            .Select(dbo => dbo.ToMemory())
            .ToListAsync();
    }
}