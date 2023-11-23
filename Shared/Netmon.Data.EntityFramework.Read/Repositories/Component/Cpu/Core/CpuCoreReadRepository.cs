using Microsoft.EntityFrameworkCore;
using Netmon.Data.DBO.Component.Cpu.Core;
using Netmon.Data.EntityFramework.Database;
using Netmon.Data.Repositories.Read.Component.Cpu.Core;

namespace Netmon.Data.EntityFramework.Read.Repositories.Component.Cpu.Core;

public class CpuCoreReadRepository : ICpuCoreReadRepository
{
    private readonly DevicesDatabase _database;
    
    private readonly ICpuCoreMetricReadRepository _cpuCoreMetricsReadRepository;

    public CpuCoreReadRepository(DevicesDatabase database, ICpuCoreMetricReadRepository cpuCoreMetricsReadRepository)
    {
        _database = database;
        _cpuCoreMetricsReadRepository = cpuCoreMetricsReadRepository;
    }

    public async Task<List<CpuCoreMetricsDBO>> GetAll()
    {
        return await _database.CpuCoreMetrics.ToListAsync();
    }

    public async Task<CpuCoreMetricsDBO?> GetById(Guid id)
    {
        return await _database.CpuCoreMetrics.FirstOrDefaultAsync(device => device.Id == id);
    }
}