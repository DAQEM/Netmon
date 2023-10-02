using DevicesLib.Database;
using DevicesLib.DBO.Component.Cpu.Core;

namespace DevicesLib.Repositories.Component.Cpu.Core;

public class CpuCoreMetricsRepository : ICpuCoreMetricsRepository
{
    private readonly DevicesDatabase _database;

    public CpuCoreMetricsRepository(DevicesDatabase database)
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