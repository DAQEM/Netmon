using DevicesLib.Database;
using DevicesLib.DBO.Component.Memory;

namespace DevicesLib.Repositories.Component.Memory;

public class MemoryMetricsRepository : IMemoryMetricsRepository
{
    private readonly DevicesDatabase _database;

    public MemoryMetricsRepository(DevicesDatabase database)
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