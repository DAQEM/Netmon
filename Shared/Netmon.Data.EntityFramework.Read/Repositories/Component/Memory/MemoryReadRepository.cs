using Microsoft.EntityFrameworkCore;
using Netmon.Data.DBO.Component.Memory;
using Netmon.Data.EntityFramework.Database;
using Netmon.Data.Repositories.Read.Component.Memory;

namespace Netmon.Data.EntityFramework.Read.Repositories.Component.Memory;

public class MemoryReadRepository(DevicesDatabase database) : IMemoryReadRepository
{
    public async Task<List<MemoryDBO>> GetAll()
    {
        return await database.Memory
            .ToListAsync();
    }

    public async Task<MemoryDBO?> GetById(Guid id)
    {
        return await database.Memory
            .FirstOrDefaultAsync(device => device.Id == id);
    }
    
    public async Task<List<MemoryDBO>> GetByDeviceId(Guid deviceId)
    {
        return await database.Memory
            .Where(memory => memory.DeviceId == deviceId)
            .ToListAsync();
    }

    public async Task<List<MemoryDBO>> GetByDeviceIdWithMetrics(Guid deviceId)
    {
        return await database.Memory
            .Include(memory => memory.MemoryMetrics)
            .Where(memory => memory.DeviceId == deviceId)
            .ToListAsync();
    }
    
    public async Task<List<MemoryDBO>> GetByDeviceIdWithMetrics(Guid deviceId, DateTime from, DateTime to)
    {
        return await database.Memory
            .Include(memory => memory.MemoryMetrics)
            .Where(memory => memory.DeviceId == deviceId)
            .Where(memory => memory.MemoryMetrics.Any(metric => metric.Timestamp >= from && metric.Timestamp <= to))
            .ToListAsync();
    }
}