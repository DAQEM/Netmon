using Netmon.Data.DBO.Component.Memory;
using Netmon.Data.EntityFramework.Database;
using Netmon.Data.Repositories.Write.Component.Memory;

namespace Netmon.Data.Write.Repositories.Component.Memory;

public class MemoryMetricsWriteRepository : IMemoryMetricsWriteRepository
{
    private readonly DevicesDatabase _database;

    public MemoryMetricsWriteRepository(DevicesDatabase database)
    {
        _database = database;
    }

    public async Task Add(MemoryMetricsDBO memoryMetrics)
    {
        if (memoryMetrics == null)
        {
            throw new ArgumentNullException(nameof(memoryMetrics));
        }
        
        await _database.MemoryMetrics.AddAsync(memoryMetrics);
    }
    
    public async Task SaveChanges()
    {
        await _database.SaveChangesAsync();
    }
}