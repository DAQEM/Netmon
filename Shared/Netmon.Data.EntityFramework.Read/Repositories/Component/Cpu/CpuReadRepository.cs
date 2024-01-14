using Microsoft.EntityFrameworkCore;
using Netmon.Data.DBO.Component.Cpu;
using Netmon.Data.EntityFramework.Database;
using Netmon.Data.Repositories.Read.Component.Cpu;

namespace Netmon.Data.EntityFramework.Read.Repositories.Component.Cpu;

public class CpuReadRepository(DevicesDatabase database) : ICpuReadRepository
{
    public async Task<List<CpuDBO>> GetAll()
    {
        return await database.Cpus
            .ToListAsync();
    }

    public async Task<CpuDBO?> GetById(Guid id)
    {
        return await database.Cpus
            .FirstOrDefaultAsync(device => device.Id == id);
    }

    public async Task<List<CpuDBO>> GetByDeviceId(Guid deviceId)
    {
        return await database.Cpus
            .Where(cpu => cpu.DeviceId == deviceId)
            .ToListAsync();
    }

    public async Task<List<CpuDBO>> GetByDeviceIdWithMetrics(Guid deviceId)
    {
        return await database.Cpus
            .Include(cpu => cpu.CpuMetrics)
            .Where(cpu => cpu.DeviceId == deviceId)
            .ToListAsync();
    }

    public async Task<List<CpuDBO>> GetByDeviceIdWithMetrics(Guid deviceId, DateTime from, DateTime to)
    {
        return await database.Cpus
            .Include(cpu => cpu.CpuMetrics)
            .Where(cpu => cpu.DeviceId == deviceId)
            .Where(cpu => cpu.CpuMetrics.Any(metric => metric.Timestamp >= from && metric.Timestamp <= to))
            .ToListAsync();
    }
}