﻿using Microsoft.EntityFrameworkCore;
using Netmon.Data.DBO.Component.Disk;
using Netmon.Data.EntityFramework.Database;
using Netmon.Data.Repositories.Read.Component.Disk;

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
        return await _database.DiskMetrics.ToListAsync();
    }
}