using Microsoft.EntityFrameworkCore;
using Netmon.Data.DBO.Component.Disk;
using Netmon.Data.EntityFramework.Database;
using Netmon.Data.Repositories.Read.Component.Disk;
using Netmon.Models.Component.Disk;

namespace Netmon.Data.EntityFramework.Read.Repositories.Component.Disk;

public class DiskReadRepository : IDiskReadRepository
{
    private readonly DevicesDatabase _database;
    
    private readonly IDiskMetricReadRepository _diskMetricsReadRepository;

    public DiskReadRepository(DevicesDatabase database, IDiskMetricReadRepository diskMetricsReadRepository)
    {
        _database = database;
        _diskMetricsReadRepository = diskMetricsReadRepository;
    }

    public async Task<List<DiskDBO>> GetAll()
    {
        return await _database.Disks.ToListAsync();
    }

    public async Task<DiskDBO?> GetById(Guid id)
    {
        return await _database.Disks.FirstOrDefaultAsync(device => device.Id == id);
    }
    
    public async Task<List<DiskDBO>> GetByDeviceId(Guid deviceId)
    {
        return await _database.Disks.Where(disk => disk.DeviceId == deviceId).ToListAsync();
    }

    public async Task<List<DiskDBO>> GetByDeviceIdWithMetrics(Guid deviceId)
    {
        return await _database.Disks.Include(disk => disk.DiskMetrics).Where(disk => disk.DeviceId == deviceId).ToListAsync();
    }
    
    public async Task<List<DiskDBO>> GetByDeviceIdWithMetrics(Guid deviceId, DateTime from, DateTime to)
    {
        return await _database.Disks
            .Include(disk => disk.DiskMetrics)
            .Where(disk => disk.DeviceId == deviceId)
            .Select(disk => new DiskDBO
            {
                Index = disk.Index,
                MountingPoint = disk.MountingPoint,
                DiskMetrics = disk.DiskMetrics
                    .Where(metric => metric.Timestamp >= from && metric.Timestamp <= to)
                    .Select(metric => new DiskMetricsDBO
                {
                    Timestamp = metric.Timestamp,
                    AllocationUnits = metric.AllocationUnits,
                    TotalSpace = metric.TotalSpace,
                    UsedSpace = metric.UsedSpace
                })
                    .OrderBy(metric => metric.Timestamp)
                    .ToList()
            })
            .ToListAsync();
    }
}