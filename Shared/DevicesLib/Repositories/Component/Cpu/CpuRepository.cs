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
        
        CpuDBO? existingCpu = await _database.Cpus.FirstOrDefaultAsync(c => c.Id == cpu.Id);

        if (existingCpu == null)
        {
            _database.Entry(cpu).CurrentValues.SetValues(cpu);
        }
        else
        {
            await _database.Cpus.AddAsync(cpu);
        }
        
        await _database.SaveChangesAsync();

        if (cpu.CpuMetrics != null!)
        {
            foreach (CpuMetricsDBO cpuMetrics in cpu.CpuMetrics)
            {
                await _cpuMetricsRepository.Add(cpuMetrics);
            }
        }
        
        if (cpu.CpuCores != null!)
        {
            foreach (CpuCoreDBO cpuCore in cpu.CpuCores)
            {
                await _cpuCoreRepository.AddOrUpdateCpu(cpuCore);
            }
        }
    }
}