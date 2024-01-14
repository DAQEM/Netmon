using Microsoft.EntityFrameworkCore;
using Netmon.Data.DBO.Component.Cpu;
using Netmon.Data.DBO.Component.Cpu.Core;
using Netmon.Data.EntityFramework.Database;
using Netmon.Data.Repositories.Write.Component.Cpu;
using Netmon.Data.Repositories.Write.Component.Cpu.Core;

namespace Netmon.Data.Write.Repositories.Component.Cpu;

public class CpuWriteRepository(
    DevicesDatabase database,
    ICpuMetricsWriteRepository cpuMetricsWriteRepository,
    ICpuCoreWriteRepository cpuCoreWriteRepository)
    : ICpuWriteRepository
{
    public async Task AddOrUpdate(CpuDBO cpu)
    {
        if (cpu is null)
        {
            throw new ArgumentNullException(nameof(cpu));
        }
        
        CpuDBO? existingCpu = await database.Cpus.FirstOrDefaultAsync(c => c.DeviceId == cpu.DeviceId && c.Index == cpu.Index);

        if (existingCpu is null)
        {
            cpu.Id = Guid.NewGuid();
            await database.Cpus.AddAsync(cpu);
        }
        else
        {
            cpu.Id = existingCpu.Id;
            database.Entry(existingCpu).CurrentValues.SetValues(cpu);
            
            if (cpu.CpuMetrics is not null)
            {
                foreach (CpuMetricsDBO cpuMetrics in cpu.CpuMetrics)
                {
                    cpuMetrics.CpuId = cpu.Id;
                    await cpuMetricsWriteRepository.Add(cpuMetrics);
                }
            }
        }
        
        if (cpu.CpuCores is not null)
        {
            foreach (CpuCoreDBO cpuCore in cpu.CpuCores)
            {
                cpuCore.CpuId = cpu.Id;
                await cpuCoreWriteRepository.AddOrUpdate(cpuCore);
            }
        }
    }
    
    public async Task SaveChanges()
    {
        await database.SaveChangesAsync();
    }
}