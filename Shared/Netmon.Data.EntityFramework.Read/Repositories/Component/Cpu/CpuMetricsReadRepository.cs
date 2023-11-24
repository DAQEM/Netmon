using Microsoft.EntityFrameworkCore;
using Netmon.Data.EntityFramework.Database;
using Netmon.Data.Repositories.Read.Component.Cpu;
using Netmon.Models.Component.Cpu.Metric;

namespace Netmon.Data.EntityFramework.Read.Repositories.Component.Cpu;

public class CpuMetricsReadRepository : ICpuMetricReadRepository
{
    private readonly DevicesDatabase _database;

    public CpuMetricsReadRepository(DevicesDatabase database)
    {
        _database = database;
    }

    public async Task<List<ICpuMetric>> GetAll()
    {
        return await _database.CpuMetrics.Select(dbo => dbo.ToCpuMetric()).ToListAsync();
    }

    public async Task<ICpuMetric?> GetById(Guid id)
    {
        return (await _database.CpuMetrics.FirstOrDefaultAsync(device => device.Id == id))?.ToCpuMetric();
    }

    public async Task<List<ICpuMetric>> GetByComponentId(Guid componentId)
    {
        return await _database.CpuMetrics.Where(cpu => cpu.CpuId == componentId).Select(dbo => dbo.ToCpuMetric()).ToListAsync();
    }

    public async Task<List<ICpuMetric>> GetByComponentIds(List<Guid> componentIds)
    {
        return await _database.CpuMetrics.Where(cpu => componentIds.Contains(cpu.CpuId)).Select(dbo => dbo.ToCpuMetric()).ToListAsync();
    }
}