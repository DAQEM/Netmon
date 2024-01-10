using Microsoft.EntityFrameworkCore;
using Netmon.Data.DBO.Component.Cpu.Core;
using Netmon.Data.EntityFramework.Database;
using Netmon.Data.Repositories.Write.Component.Cpu.Core;
using Netmon.Models.Component.Cpu.Core;

namespace Netmon.Data.Write.Repositories.Component.Cpu.Core;

public class CpuCoreWriteRepository : ICpuCoreWriteRepository
{
    private readonly DevicesDatabase _database;
    
    private readonly ICpuCoreMetricsWriteRepository _cpuCoreMetricsWriteRepository;

    public CpuCoreWriteRepository(DevicesDatabase database, ICpuCoreMetricsWriteRepository cpuCoreMetricsWriteRepository)
    {
        _database = database;
        _cpuCoreMetricsWriteRepository = cpuCoreMetricsWriteRepository;
    }

    public async Task AddOrUpdate(CpuCoreDBO cpuCore)
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
                    await _cpuCoreMetricsWriteRepository.Add(cpuCoreMetrics);
                }
            }
        }
    }

    public async Task SaveChanges()
    {
        await _database.SaveChangesAsync();
    }
}