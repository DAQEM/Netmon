﻿using Microsoft.EntityFrameworkCore;
using Netmon.Data.DBO.Component.Disk;
using Netmon.Data.EntityFramework.Database;
using Netmon.Data.Repositories.Read.Component.Disk;

namespace Netmon.Data.EntityFramework.Read.Repositories.Component.Disk;

public class DiskReadRepository(DevicesDatabase database, IDiskMetricReadRepository diskMetricsReadRepository)
    : IDiskReadRepository
{
    private readonly IDiskMetricReadRepository _diskMetricsReadRepository = diskMetricsReadRepository;

    public async Task<List<DiskDBO>> GetAll()
    {
        return await database.Disks.ToListAsync();
    }

    public async Task<DiskDBO?> GetById(Guid id)
    {
        return await database.Disks.FirstOrDefaultAsync(device => device.Id == id);
    }
    
    public async Task<List<DiskDBO>> GetByDeviceId(Guid deviceId)
    {
        return await database.Disks.Where(disk => disk.DeviceId == deviceId).ToListAsync();
    }

    public async Task<List<DiskDBO>> GetByDeviceIdWithMetrics(Guid deviceId)
    {
        return await database.Disks.Include(disk => disk.DiskMetrics).Where(disk => disk.DeviceId == deviceId).ToListAsync();
    }
    
    public async Task<List<DiskDBO>> GetByDeviceIdWithMetrics(Guid deviceId, DateTime from, DateTime to)
    {
        return await database.Disks
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