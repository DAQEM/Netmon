using Microsoft.EntityFrameworkCore;
using Netmon.Data.DBO.Component.Cpu;
using Netmon.Data.DBO.Component.Cpu.Core;
using Netmon.Data.EntityFramework.Database;
using Netmon.Data.Repositories.Read.Component.Cpu;
using Netmon.Data.Repositories.Read.Component.Cpu.Core;

namespace Netmon.Data.EntityFramework.Read.Repositories.Component.Cpu;

public class CpuReadRepository : ICpuReadRepository
{
    private readonly DevicesDatabase _database;
    
    private readonly ICpuMetricReadRepository _cpuMetricsReadRepository;
    private readonly ICpuCoreReadRepository _cpuCoreReadRepository;

    public CpuReadRepository(DevicesDatabase database, ICpuMetricReadRepository cpuMetricsReadRepository, 
        ICpuCoreReadRepository cpuCoreReadRepository)
    {
        _database = database;
        _cpuMetricsReadRepository = cpuMetricsReadRepository;
        _cpuCoreReadRepository = cpuCoreReadRepository;
    }

    public async Task<List<CpuDBO>> GetAll()
    {
        return await _database.Cpus.ToListAsync();
    }
}