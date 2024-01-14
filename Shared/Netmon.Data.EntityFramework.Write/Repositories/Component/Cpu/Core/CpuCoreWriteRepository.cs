using Microsoft.EntityFrameworkCore;
using Netmon.Data.DBO.Component.Cpu.Core;
using Netmon.Data.EntityFramework.Database;
using Netmon.Data.Repositories.Write.Component.Cpu.Core;

namespace Netmon.Data.Write.Repositories.Component.Cpu.Core;

public class CpuCoreWriteRepository(
    DevicesDatabase database,
    ICpuCoreMetricsWriteRepository cpuCoreMetricsWriteRepository)
    : ICpuCoreWriteRepository
{
    public async Task AddOrUpdate(CpuCoreDBO cpuCore)
    {
        if (cpuCore is null)
        {
            throw new ArgumentNullException(nameof(cpuCore));
        }
        
        CpuCoreDBO? existingCpuCore = await database.CpuCores.FirstOrDefaultAsync(c => c.CpuId == cpuCore.CpuId && c.Index == cpuCore.Index);

        if (existingCpuCore is null)
        {
            cpuCore.Id = Guid.NewGuid();
            await database.CpuCores.AddAsync(cpuCore);
        }
        else
        {
            cpuCore.Id = existingCpuCore.Id;
            database.Entry(existingCpuCore).CurrentValues.SetValues(cpuCore);
            
            if (cpuCore.CpuCoreMetrics is not null)
            {
                foreach (CpuCoreMetricsDBO cpuCoreMetrics in cpuCore.CpuCoreMetrics)
                {
                    cpuCoreMetrics.CpuCoreId = cpuCore.Id;
                    await cpuCoreMetricsWriteRepository.Add(cpuCoreMetrics);
                }
            }
        }
    }

    public async Task SaveChanges()
    {
        await database.SaveChangesAsync();
    }
}