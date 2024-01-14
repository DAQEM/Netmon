using Microsoft.EntityFrameworkCore;
using Netmon.Data.DBO.Component.Cpu;
using Netmon.Data.EntityFramework.Database;
using Netmon.Data.Repositories.Read.Component.Cpu;

namespace Netmon.Data.EntityFramework.Read.Repositories.Component.Cpu;

public class CpuMetricsReadRepository(DevicesDatabase database) : ICpuMetricReadRepository
{
    public async Task<List<CpuMetricsDBO>> GetAll()
    {
        return await database.CpuMetrics.ToListAsync();
    }

    public async Task<CpuMetricsDBO?> GetById(Guid id)
    {
        return await database.CpuMetrics.FirstOrDefaultAsync(device => device.Id == id);
    }

    public async Task<List<CpuMetricsDBO>> GetByComponentId(Guid componentId)
    {
        return await database.CpuMetrics.Where(cpu => cpu.CpuId == componentId).ToListAsync();
    }

    public async Task<List<CpuMetricsDBO>> GetByComponentIds(List<Guid> componentIds)
    {
        return await database.CpuMetrics.Where(cpu => componentIds.Contains(cpu.CpuId)).ToListAsync();
    }
}