using Microsoft.EntityFrameworkCore;
using Netmon.Data.EntityFramework.Database;
using Netmon.Data.EntityFramework.DBO.Component.Cpu;
using Netmon.Data.EntityFramework.DBO.Component.Cpu.Core;
using Netmon.Data.Repositories.Write.Component.Cpu;
using Netmon.Data.Repositories.Write.Component.Cpu.Core;
using Netmon.Models.Component.Cpu;

namespace Netmon.Data.Write.Repositories.Component.Cpu;

public class CpuWriteRepository : ICpuWriteRepository
{
    private readonly DevicesDatabase _database;
    
    private readonly ICpuMetricsWriteRepository _cpuMetricsWriteRepository;
    private readonly ICpuCoreWriteRepository _cpuCoreWriteRepository;

    public CpuWriteRepository(DevicesDatabase database, ICpuMetricsWriteRepository cpuMetricsWriteRepository, 
        ICpuCoreWriteRepository cpuCoreWriteRepository)
    {
        _database = database;
        _cpuMetricsWriteRepository = cpuMetricsWriteRepository;
        _cpuCoreWriteRepository = cpuCoreWriteRepository;
    }

    public async Task AddOrUpdate(ICpu cpu)
    {
        if (cpu == null)
        {
            throw new ArgumentNullException(nameof(cpu));
        }
        
        CpuDBO cpuDBO = CpuDBO.FromCpu(cpu);
        
        CpuDBO? existingCpu = await _database.Cpus.FirstOrDefaultAsync(c => c.DeviceId == cpuDBO.DeviceId && c.Index == cpuDBO.Index);

        if (existingCpu == null)
        {
            cpuDBO.Id = Guid.NewGuid();
            await _database.Cpus.AddAsync(cpuDBO);
        }
        else
        {
            cpuDBO.Id = existingCpu.Id;
            _database.Entry(existingCpu).CurrentValues.SetValues(cpuDBO);
            
            if (cpuDBO.CpuMetrics != null!)
            {
                foreach (CpuMetricsDBO cpuMetrics in cpuDBO.CpuMetrics)
                {
                    cpuMetrics.CpuId = cpuDBO.Id;
                    await _cpuMetricsWriteRepository.Add(cpuMetrics.ToCpuMetric());
                }
            }
        }
        
        if (cpuDBO.CpuCores != null!)
        {
            foreach (CpuCoreDBO cpuCore in cpuDBO.CpuCores)
            {
                cpuCore.CpuId = cpuDBO.Id;
                await _cpuCoreWriteRepository.AddOrUpdate(cpuCore.ToCpuCore());
            }
        }
    }
    
    public async Task SaveChanges()
    {
        await _database.SaveChangesAsync();
    }
}