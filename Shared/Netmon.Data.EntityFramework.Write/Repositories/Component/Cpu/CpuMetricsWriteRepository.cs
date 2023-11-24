using Netmon.Data.EntityFramework.Database;
using Netmon.Data.EntityFramework.DBO.Component.Cpu;
using Netmon.Data.Repositories.Write.Component.Cpu;
using Netmon.Models.Component.Cpu.Metric;

namespace Netmon.Data.Write.Repositories.Component.Cpu;

public class CpuMetricsWriteRepository : ICpuMetricsWriteRepository
{
    private readonly DevicesDatabase _database;

    public CpuMetricsWriteRepository(DevicesDatabase database)
    {
        _database = database;
    }

    public async Task Add(ICpuMetric cpuMetrics)
    {
        if (cpuMetrics == null)
        {
            throw new ArgumentNullException(nameof(cpuMetrics));
        }
        
        CpuMetricsDBO cpuMetricsDBO = CpuMetricsDBO.FromCpuMetric(cpuMetrics);
        
        await _database.CpuMetrics.AddAsync(cpuMetricsDBO);
    }

    public async Task SaveChanges()
    {
        await _database.SaveChangesAsync();
    }
}