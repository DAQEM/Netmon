using Netmon.Data.EntityFramework.Database;
using Netmon.Data.EntityFramework.DBO.Component.Cpu.Core;
using Netmon.Data.Repositories.Write.Component.Cpu.Core;
using Netmon.Models.Component.Cpu.Core.Metric;

namespace Netmon.Data.Write.Repositories.Component.Cpu.Core;

public class CpuCoreMetricsWriteRepository : ICpuCoreMetricsWriteRepository
{
    private readonly DevicesDatabase _database;

    public CpuCoreMetricsWriteRepository(DevicesDatabase database)
    {
        _database = database;
    }

    public async Task Add(ICpuCoreMetric cpuCoreMetrics)
    {
        if (cpuCoreMetrics == null)
        {
            throw new ArgumentNullException(nameof(cpuCoreMetrics));
        }
        
        CpuCoreMetricsDBO cpuCoreMetricsDBO = CpuCoreMetricsDBO.FromCpuCoreMetric(cpuCoreMetrics);
        
        await _database.CpuCoreMetrics.AddAsync(cpuCoreMetricsDBO);
    }

    public async Task SaveChanges()
    {
        await _database.SaveChangesAsync();
    }
}