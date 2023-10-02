using DevicesLib.Database;
using DevicesLib.DBO.Component.Cpu.Core;
using Microsoft.EntityFrameworkCore;

namespace DevicesLib.Repositories.Component.Cpu.Core;

public class CpuCoreRepository : ICpuCoreRepository
{
    private readonly DevicesDatabase _database;
    
    private readonly ICpuCoreMetricsRepository _cpuCoreMetricsRepository;

    public CpuCoreRepository(DevicesDatabase database, ICpuCoreMetricsRepository cpuCoreMetricsRepository)
    {
        _database = database;
        _cpuCoreMetricsRepository = cpuCoreMetricsRepository;
    }

    public async Task AddOrUpdateCpu(CpuCoreDBO cpuCore)
    {
        if (cpuCore == null)
        {
            throw new ArgumentNullException(nameof(cpuCore));
        }
        
        CpuCoreDBO? existingCpuCore = await _database.CpuCores.FirstOrDefaultAsync(c => c.CpuId == cpuCore.CpuId && c.Index == cpuCore.Index);

        if (existingCpuCore == null)
        {
            cpuCore.Id = Guid.NewGuid();
            await _database.CpuCores.AddAsync(cpuCore);
        }
        else
        {
            cpuCore.Id = existingCpuCore.Id;
            _database.Entry(existingCpuCore).CurrentValues.SetValues(cpuCore);
            
            if (cpuCore.CpuCoreMetrics != null!)
            {
                foreach (CpuCoreMetricsDBO cpuCoreMetrics in cpuCore.CpuCoreMetrics)
                {
                    cpuCoreMetrics.CpuCoreId = cpuCore.Id;
                    await _cpuCoreMetricsRepository.Add(cpuCoreMetrics);
                }
            }
        }
    }

    public async Task SaveChanges()
    {
        await _database.SaveChangesAsync();
    }
}