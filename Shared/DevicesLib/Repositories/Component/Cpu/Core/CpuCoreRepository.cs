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
        
        CpuCoreDBO? existingCpuCore = await _database.CpuCores.FirstOrDefaultAsync(c => c.Id == cpuCore.Id);

        if (existingCpuCore == null)
        {
            _database.Entry(cpuCore).CurrentValues.SetValues(cpuCore);
        }
        else
        {
            await _database.CpuCores.AddAsync(cpuCore);
        }
        
        await _database.SaveChangesAsync();
        
        if (cpuCore.CpuCoreMetrics != null!)
        {
            foreach (CpuCoreMetricsDBO cpuCoreMetrics in cpuCore.CpuCoreMetrics)
            {
                await _cpuCoreMetricsRepository.Add(cpuCoreMetrics);
            }
        }
    }
}