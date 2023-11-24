using Microsoft.EntityFrameworkCore;
using Netmon.Data.EntityFramework.Database;
using Netmon.Data.EntityFramework.DBO.Component.Cpu.Core;
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

    public async Task AddOrUpdate(ICpuCore cpuCore)
    {
        if (cpuCore == null)
        {
            throw new ArgumentNullException(nameof(cpuCore));
        }
        
        CpuCoreDBO cpuCoreDBO = CpuCoreDBO.FromCpuCore(cpuCore);
        
        CpuCoreDBO? existingCpuCore = await _database.CpuCores.FirstOrDefaultAsync(c => c.CpuId == cpuCoreDBO.CpuId && c.Index == cpuCoreDBO.Index);

        if (existingCpuCore == null)
        {
            cpuCoreDBO.Id = Guid.NewGuid();
            await _database.CpuCores.AddAsync(cpuCoreDBO);
        }
        else
        {
            cpuCoreDBO.Id = existingCpuCore.Id;
            _database.Entry(existingCpuCore).CurrentValues.SetValues(cpuCoreDBO);
            
            if (cpuCoreDBO.CpuCoreMetrics != null!)
            {
                foreach (CpuCoreMetricsDBO cpuCoreMetrics in cpuCoreDBO.CpuCoreMetrics)
                {
                    cpuCoreMetrics.CpuCoreId = cpuCoreDBO.Id;
                    await _cpuCoreMetricsWriteRepository.Add(cpuCoreMetrics.ToCpuCoreMetric());
                }
            }
        }
    }

    public async Task SaveChanges()
    {
        await _database.SaveChangesAsync();
    }
}