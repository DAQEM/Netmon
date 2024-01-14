﻿using Microsoft.EntityFrameworkCore;
using Netmon.Data.DBO.Component.Cpu.Core;
using Netmon.Data.EntityFramework.Database;
using Netmon.Data.Repositories.Read.Component.Cpu.Core;

namespace Netmon.Data.EntityFramework.Read.Repositories.Component.Cpu.Core;

public class CpuCoreReadRepository(DevicesDatabase database, ICpuCoreMetricReadRepository cpuCoreMetricsReadRepository)
    : ICpuCoreReadRepository
{
    private readonly ICpuCoreMetricReadRepository _cpuCoreMetricsReadRepository = cpuCoreMetricsReadRepository;

    public async Task<List<CpuCoreMetricsDBO>> GetAll()
    {
        return await database.CpuCoreMetrics.ToListAsync();
    }

    public async Task<CpuCoreMetricsDBO?> GetById(Guid id)
    {
        return await database.CpuCoreMetrics.FirstOrDefaultAsync(device => device.Id == id);
    }
}