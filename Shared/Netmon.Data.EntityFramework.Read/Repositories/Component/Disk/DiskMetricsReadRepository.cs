using Microsoft.EntityFrameworkCore;
using Netmon.Data.EntityFramework.Database;
using Netmon.Data.EntityFramework.DBO.Component.Disk;
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

    public async Task<List<IDiskMetric>> GetAll()
    {
        return await _database.DiskMetrics
            .Select(dbo => dbo.ToDiskMetric())
            .ToListAsync();
    }

    public async Task<IDiskMetric?> GetById(Guid id)
    {
        return (await _database.DiskMetrics
            .FirstOrDefaultAsync(device => device.Id == id))?
            .ToDiskMetric();
    }
    
    public async Task<List<IDiskMetric>> GetByComponentId(Guid componentId)
    {
        return await _database.DiskMetrics
            .Where(disk => disk.DiskId == componentId)
            .Select(dbo => dbo.ToDiskMetric())
            .ToListAsync();
    }

    public async Task<List<IDiskMetric>> GetByComponentIds(List<Guid> componentIds)
    {
        return await _database.DiskMetrics
            .Where(disk => componentIds.Contains(disk.DiskId))
            .Select(dbo => dbo.ToDiskMetric())
            .ToListAsync();
    }
}