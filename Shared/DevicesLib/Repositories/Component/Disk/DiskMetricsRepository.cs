using DevicesLib.Database;
using DevicesLib.DBO.Component.Disk;

namespace DevicesLib.Repositories.Component.Disk;

public class DiskMetricsRepository : IDiskMetricsRepository
{
    private readonly DevicesDatabase _database;

    public DiskMetricsRepository(DevicesDatabase database)
    {
        _database = database;
    }

    public async Task Add(DiskMetricsDBO diskMetrics)
    {
        if (diskMetrics == null)
        {
            throw new ArgumentNullException(nameof(diskMetrics));
        }
        
        await _database.DiskMetrics.AddAsync(diskMetrics);
    }
    
    public async Task SaveChanges()
    {
        await _database.SaveChangesAsync();
    }
}