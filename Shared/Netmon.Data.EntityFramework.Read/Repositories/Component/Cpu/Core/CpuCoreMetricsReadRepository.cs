using Microsoft.EntityFrameworkCore;
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

    public async Task<List<ICpuCoreMetric>> GetAll()
    {
        return await _database.CpuCoreMetrics.Select(dbo => dbo.ToCpuCoreMetric()).ToListAsync();
    }

    public async Task<ICpuCoreMetric?> GetById(Guid id)
    {
        return (await _database.CpuCoreMetrics.FirstOrDefaultAsync(device => device.Id == id))?.ToCpuCoreMetric();
    }
}