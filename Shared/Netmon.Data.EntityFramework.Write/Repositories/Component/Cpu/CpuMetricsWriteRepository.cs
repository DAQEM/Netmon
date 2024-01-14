using Netmon.Data.DBO.Component.Cpu;
using Netmon.Data.EntityFramework.Database;
using Netmon.Data.Repositories.Write.Component.Cpu;

namespace Netmon.Data.Write.Repositories.Component.Cpu;

public class CpuMetricsWriteRepository(DevicesDatabase database) : ICpuMetricsWriteRepository
{
    public async Task Add(CpuMetricsDBO cpuMetrics)
    {
        if (cpuMetrics is null)
        {
            throw new ArgumentNullException(nameof(cpuMetrics));
        }
        
        await database.CpuMetrics.AddAsync(cpuMetrics);
    }

    public async Task SaveChanges()
    {
        await database.SaveChangesAsync();
    }
}