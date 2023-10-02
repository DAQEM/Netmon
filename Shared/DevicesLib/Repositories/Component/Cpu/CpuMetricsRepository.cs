using DevicesLib.Database;
using DevicesLib.DBO.Component.Cpu;

namespace DevicesLib.Repositories.Component.Cpu;

public class CpuMetricsRepository : ICpuMetricsRepository
{
    private readonly DevicesDatabase _database;

    public CpuMetricsRepository(DevicesDatabase database)
    {
        _database = database;
    }

    public async Task Add(CpuMetricsDBO cpuMetrics)
    {
        if (cpuMetrics == null)
        {
            throw new ArgumentNullException(nameof(cpuMetrics));
        }
        
        await _database.CpuMetrics.AddAsync(cpuMetrics);
    }

    public async Task SaveChanges()
    {
        await _database.SaveChangesAsync();
    }
}