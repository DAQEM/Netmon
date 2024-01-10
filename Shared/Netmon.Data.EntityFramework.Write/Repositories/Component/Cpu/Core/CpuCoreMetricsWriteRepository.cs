using Netmon.Data.DBO.Component.Cpu.Core;
using Netmon.Data.EntityFramework.Database;
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

    public async Task Add(CpuCoreMetricsDBO cpuCoreMetrics)
    {
        if (cpuCoreMetrics == null)
        {
            throw new ArgumentNullException(nameof(cpuCoreMetrics));
        }
        
        await _database.CpuCoreMetrics.AddAsync(cpuCoreMetrics);
    }

    public async Task SaveChanges()
    {
        await _database.SaveChangesAsync();
    }
}