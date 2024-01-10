﻿using Microsoft.EntityFrameworkCore;
using Netmon.Data.DBO.Component.Cpu.Core;
using Netmon.Data.EntityFramework.Database;
using Netmon.Data.Repositories.Read.Component.Cpu.Core;
using Netmon.Models.Component.Cpu.Core.Metric;

namespace Netmon.Data.EntityFramework.Read.Repositories.Component.Cpu.Core;

public class CpuCoreMetricsReadRepository : ICpuCoreMetricReadRepository
{
    private readonly DevicesDatabase _database;

    public CpuCoreMetricsReadRepository(DevicesDatabase database)
    {
        _database = database;
    }

    public async Task<List<CpuCoreMetricsDBO>> GetAll()
    {
        return await _database.CpuCoreMetrics.ToListAsync();
    }

    public async Task<CpuCoreMetricsDBO?> GetById(Guid id)
    {
        return await _database.CpuCoreMetrics.FirstOrDefaultAsync(device => device.Id == id);
    }
}