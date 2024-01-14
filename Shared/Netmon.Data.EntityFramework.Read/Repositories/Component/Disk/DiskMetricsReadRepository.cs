using Microsoft.EntityFrameworkCore;
using Netmon.Data.DBO.Component.Disk;
using Netmon.Data.EntityFramework.Database;
using Netmon.Data.Repositories.Read.Component.Disk;

namespace Netmon.Data.EntityFramework.Read.Repositories.Component.Disk;

public class DiskMetricsReadRepository(DevicesDatabase database) : IDiskMetricReadRepository
{
    public async Task<List<DiskMetricsDBO>> GetAll()
    {
        return await database.DiskMetrics
            .ToListAsync();
    }

    public async Task<DiskMetricsDBO?> GetById(Guid id)
    {
        return await database.DiskMetrics
            .FirstOrDefaultAsync(device => device.Id == id);
    }
    
    public async Task<List<DiskMetricsDBO>> GetByComponentId(Guid componentId)
    {
        return await database.DiskMetrics
            .Where(disk => disk.DiskId == componentId)
            .ToListAsync();
    }

    public async Task<List<DiskMetricsDBO>> GetByComponentIds(List<Guid> componentIds)
    {
        return await database.DiskMetrics
            .Where(disk => componentIds.Contains(disk.DiskId))
            .ToListAsync();
    }
}