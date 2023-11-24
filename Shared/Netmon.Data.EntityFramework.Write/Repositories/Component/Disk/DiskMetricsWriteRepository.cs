using Netmon.Data.EntityFramework.Database;
using Netmon.Data.EntityFramework.DBO.Component.Disk;
using Netmon.Data.Repositories.Write.Component.Disk;
using Netmon.Models.Component.Disk.Metric;

namespace Netmon.Data.Write.Repositories.Component.Disk;

public class DiskMetricsWriteRepository : IDiskMetricsWriteRepository
{
    private readonly DevicesDatabase _database;

    public DiskMetricsWriteRepository(DevicesDatabase database)
    {
        _database = database;
    }

    public async Task Add(IDiskMetric diskMetrics)
    {
        if (diskMetrics == null)
        {
            throw new ArgumentNullException(nameof(diskMetrics));
        }
        
        DiskMetricsDBO diskMetricsDBO = DiskMetricsDBO.FromDiskMetric(diskMetrics);
        
        await _database.DiskMetrics.AddAsync(diskMetricsDBO);
    }
    
    public async Task SaveChanges()
    {
        await _database.SaveChangesAsync();
    }
}