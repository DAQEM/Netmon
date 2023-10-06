using Microsoft.EntityFrameworkCore;
using Netmon.Data.DBO.Component.Cpu;
using Netmon.Data.DBO.Component.Cpu.Core;
using Netmon.Data.EntityFramework.Database;
using Netmon.Data.Repositories.Write.Component.Cpu;
using Netmon.Data.Repositories.Write.Component.Cpu.Core;
using Netmon.Data.Write.Repositories.Component.Cpu.Core;

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

    public async Task AddOrUpdate(CpuDBO cpu)
    {
        if (cpu == null)
        {
            throw new ArgumentNullException(nameof(cpu));
        }
        
        CpuDBO? existingCpu = await _database.Cpus.FirstOrDefaultAsync(c => c.DeviceId == cpu.DeviceId && c.Index == cpu.Index);

        if (existingCpu == null)
        {
            cpu.Id = Guid.NewGuid();
            await _database.Cpus.AddAsync(cpu);
        }
        else
        {
            cpu.Id = existingCpu.Id;
            _database.Entry(existingCpu).CurrentValues.SetValues(cpu);
            
            if (cpu.CpuMetrics != null!)
            {
                foreach (CpuMetricsDBO cpuMetrics in cpu.CpuMetrics)
                {
                    cpuMetrics.CpuId = cpu.Id;
                    await _cpuMetricsWriteRepository.Add(cpuMetrics);
                }
            }
        }
        
        if (cpu.CpuCores != null!)
        {
            foreach (CpuCoreDBO cpuCore in cpu.CpuCores)
            {
                cpuCore.CpuId = cpu.Id;
                await _cpuCoreWriteRepository.AddOrUpdate(cpuCore);
            }
        }
    }
    
    public async Task SaveChanges()
    {
        await _database.SaveChangesAsync();
    }
}