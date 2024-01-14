using Netmon.Data.DBO.Component.Cpu.Core;
using Netmon.Data.EntityFramework.Database;
using Netmon.Data.Repositories.Write.Component.Cpu.Core;

namespace Netmon.Data.Write.Repositories.Component.Cpu.Core;

public class CpuCoreMetricsWriteRepository(DevicesDatabase database) : ICpuCoreMetricsWriteRepository
{
    public async Task Add(CpuCoreMetricsDBO cpuCoreMetrics)
    {
        if (cpuCoreMetrics == null)
        {
            throw new ArgumentNullException(nameof(cpuCoreMetrics));
        }
        
        await database.CpuCoreMetrics.AddAsync(cpuCoreMetrics);
    }

    public async Task SaveChanges()
    {
        await database.SaveChangesAsync();
    }
}