using DevicesLib.Database;
using DevicesLib.DBO.Component.Cpu;
using DevicesLib.DBO.Component.Cpu.Core;
using DevicesLib.Repositories.Component.Cpu.Core;
using Microsoft.EntityFrameworkCore;

namespace DevicesLib.Repositories.Component.Cpu;

public class CpuRepository : ICpuRepository
{
    private readonly DevicesDatabase _database;
    
    private readonly ICpuMetricsRepository _cpuMetricsRepository;
    private readonly ICpuCoreRepository _cpuCoreRepository;

    public CpuRepository(DevicesDatabase database, ICpuMetricsRepository cpuMetricsRepository, 
        ICpuCoreRepository cpuCoreRepository)
    {
        _database = database;
        _cpuMetricsRepository = cpuMetricsRepository;
        _cpuCoreRepository = cpuCoreRepository;
    }

    public async Task AddOrUpdateCpu(CpuDBO cpu)
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
                    await _cpuMetricsRepository.Add(cpuMetrics);
                }
            }
        }
        
        if (cpu.CpuCores != null!)
        {
            foreach (CpuCoreDBO cpuCore in cpu.CpuCores)
            {
                cpuCore.CpuId = cpu.Id;
                await _cpuCoreRepository.AddOrUpdateCpu(cpuCore);
            }
        }
    }
    
    public async Task SaveChanges()
    {
        await _database.SaveChangesAsync();
    }
}