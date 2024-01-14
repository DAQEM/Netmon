using Netmon.Data.DBO.Component.Disk;
using Netmon.Data.EntityFramework.Database;
using Netmon.Data.Repositories.Write.Component.Disk;

namespace Netmon.Data.Write.Repositories.Component.Disk;

public class DiskMetricsWriteRepository(DevicesDatabase database) : IDiskMetricsWriteRepository
{
    public async Task Add(DiskMetricsDBO diskMetrics)
    {
        if (diskMetrics == null)
        {
            throw new ArgumentNullException(nameof(diskMetrics));
        }
        
        await database.DiskMetrics.AddAsync(diskMetrics);
    }
    
    public async Task SaveChanges()
    {
        await database.SaveChangesAsync();
    }
}