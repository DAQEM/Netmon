using Netmon.Data.EntityFramework.Database;
using Netmon.Data.EntityFramework.DBO.Component.Memory;
using Netmon.Data.Repositories.Write.Component.Memory;
using Netmon.Models.Component.Memory.Metric;

namespace Netmon.Data.Write.Repositories.Component.Memory;

public class MemoryMetricsWriteRepository : IMemoryMetricsWriteRepository
{
    private readonly DevicesDatabase _database;

    public MemoryMetricsWriteRepository(DevicesDatabase database)
    {
        _database = database;
    }

    public async Task Add(IMemoryMetric memoryMetrics)
    {
        if (memoryMetrics == null)
        {
            throw new ArgumentNullException(nameof(memoryMetrics));
        }
        
        MemoryMetricsDBO memoryMetricsDBO = MemoryMetricsDBO.FromMemoryMetric(memoryMetrics);
        
        await _database.MemoryMetrics.AddAsync(memoryMetricsDBO);
    }
    
    public async Task SaveChanges()
    {
        await _database.SaveChangesAsync();
    }
}