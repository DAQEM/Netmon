using Netmon.Data.DBO.Component.Cpu;
using Netmon.Data.EntityFramework.Database;
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