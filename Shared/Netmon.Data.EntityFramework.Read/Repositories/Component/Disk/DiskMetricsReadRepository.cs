using Microsoft.EntityFrameworkCore;
using Netmon.Data.DBO.Component.Disk;
using Netmon.Data.EntityFramework.Database;
using Netmon.Data.Repositories.Read.Component.Disk;
using Netmon.Models.Component.Disk.Metric;

namespace Netmon.Data.EntityFramework.Read.Repositories.Component.Disk;

public class DiskMetricsReadRepository : IDiskMetricReadRepository
{
    private readonly DevicesDatabase _database;

    public DiskMetricsReadRepository(DevicesDatabase database)
    {
        _database = database;
    }

    public async Task<List<DiskMetricsDBO>> GetAll()
    {
        return await _database.DiskMetrics
            .ToListAsync();
    }

    public async Task<DiskMetricsDBO?> GetById(Guid id)
    {
        return await _database.DiskMetrics
            .FirstOrDefaultAsync(device => device.Id == id);
    }
    
    public async Task<List<DiskMetricsDBO>> GetByComponentId(Guid componentId)
    {
        return await _database.DiskMetrics
            .Where(disk => disk.DiskId == componentId)
            .ToListAsync();
    }

    public async Task<List<DiskMetricsDBO>> GetByComponentIds(List<Guid> componentIds)
    {
        return await _database.DiskMetrics
            .Where(disk => componentIds.Contains(disk.DiskId))
            .ToListAsync();
    }
}