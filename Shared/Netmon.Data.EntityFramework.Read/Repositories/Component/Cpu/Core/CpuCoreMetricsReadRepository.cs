using Microsoft.EntityFrameworkCore;
using Netmon.Data.DBO.Component.Cpu.Core;
using Netmon.Data.EntityFramework.Database;
using Netmon.Data.Repositories.Read.Component.Cpu.Core;

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
}