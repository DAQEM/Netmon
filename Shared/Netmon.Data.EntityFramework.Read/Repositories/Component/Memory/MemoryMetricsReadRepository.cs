using Microsoft.EntityFrameworkCore;
using Netmon.Data.DBO.Component.Memory;
using Netmon.Data.EntityFramework.Database;
using Netmon.Data.Repositories.Read.Component.Memory;

namespace Netmon.Data.EntityFramework.Read.Repositories.Component.Memory;

public class MemoryMetricsReadRepository(DevicesDatabase database) : IMemoryMetricReadRepository
{
    public async Task<List<MemoryMetricsDBO>> GetAll()
    {
        return await database.MemoryMetrics.ToListAsync();
    }

    public async Task<MemoryMetricsDBO?> GetById(Guid id)
    {
        return await database.MemoryMetrics.FirstOrDefaultAsync(device => device.Id == id);
    }
    
    public async Task<List<MemoryMetricsDBO>> GetByComponentId(Guid componentId)
    {
        return await database.MemoryMetrics.Where(memory => memory.MemoryId == componentId).ToListAsync();
    }

    public async Task<List<MemoryMetricsDBO>> GetByComponentIds(List<Guid> componentIds)
    {
        return await database.MemoryMetrics.Where(memory => componentIds.Contains(memory.MemoryId)).ToListAsync();
    }
}