using Microsoft.EntityFrameworkCore;
using Netmon.Data.DBO.Component.Cpu;
using Netmon.Data.DBO.Device;
using Netmon.Data.EntityFramework.Database;
using Netmon.Data.Repositories.Read.Component.Cpu;

namespace Netmon.Data.EntityFramework.Read.Repositories.Component.Cpu;

public class CpuMetricsReadRepository : ICpuMetricReadRepository
{
    private readonly DevicesDatabase _database;

    public CpuMetricsReadRepository(DevicesDatabase database)
    {
        _database = database;
    }

    public async Task<List<CpuMetricsDBO>> GetAll()
    {
        return await _database.CpuMetrics.ToListAsync();
    }

    public async Task<CpuMetricsDBO?> GetById(Guid id)
    {
        return await _database.CpuMetrics.FirstOrDefaultAsync(device => device.Id == id);
    }

    public async Task<List<CpuMetricsDBO>> GetByComponentId(Guid componentId)
    {
        return await _database.CpuMetrics.Where(cpu => cpu.CpuId == componentId).ToListAsync();
    }

    public async Task<List<CpuMetricsDBO>> GetByComponentIds(List<Guid> componentIds)
    {
        return await _database.CpuMetrics.Where(cpu => componentIds.Contains(cpu.CpuId)).ToListAsync();
    }
}