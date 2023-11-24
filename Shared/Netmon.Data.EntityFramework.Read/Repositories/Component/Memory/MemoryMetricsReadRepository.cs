using Microsoft.EntityFrameworkCore;
using Netmon.Data.EntityFramework.Database;
using Netmon.Data.EntityFramework.DBO.Component.Memory;
using Netmon.Data.Repositories.Read.Component.Memory;
using Netmon.Models.Component.Memory.Metric;

namespace Netmon.Data.EntityFramework.Read.Repositories.Component.Memory;

public class MemoryMetricsReadRepository : IMemoryMetricReadRepository
{
    private readonly DevicesDatabase _database;

    public MemoryMetricsReadRepository(DevicesDatabase database)
    {
        _database = database;
    }

    public async Task<List<IMemoryMetric>> GetAll()
    {
        return await _database.MemoryMetrics.Select(dbo => dbo.ToMemoryMetric()).ToListAsync();
    }

    public async Task<IMemoryMetric?> GetById(Guid id)
    {
        return (await _database.MemoryMetrics.FirstOrDefaultAsync(device => device.Id == id))?.ToMemoryMetric();
    }
    
    public async Task<List<IMemoryMetric>> GetByComponentId(Guid componentId)
    {
        return await _database.MemoryMetrics.Where(memory => memory.MemoryId == componentId).Select(dbo => dbo.ToMemoryMetric()).ToListAsync();
    }

    public async Task<List<IMemoryMetric>> GetByComponentIds(List<Guid> componentIds)
    {
        return await _database.MemoryMetrics.Where(memory => componentIds.Contains(memory.MemoryId)).Select(dbo => dbo.ToMemoryMetric()).ToListAsync();
    }
}