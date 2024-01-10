using Microsoft.EntityFrameworkCore;
using Netmon.Data.DBO.Component.Cpu;
using Netmon.Data.EntityFramework.Database;
using Netmon.Data.Repositories.Read.Component.Cpu;
using Netmon.Data.Repositories.Read.Component.Cpu.Core;
using Netmon.Models.Component.Cpu;

namespace Netmon.Data.EntityFramework.Read.Repositories.Component.Cpu;

public class CpuReadRepository : ICpuReadRepository
{
    private readonly DevicesDatabase _database;

    public CpuReadRepository(DevicesDatabase database)
    {
        _database = database;
    }

    public async Task<List<CpuDBO>> GetAll()
    {
        return await _database.Cpus
            .ToListAsync();
    }

    public async Task<CpuDBO?> GetById(Guid id)
    {
        return await _database.Cpus
            .FirstOrDefaultAsync(device => device.Id == id);
    }

    public async Task<List<CpuDBO>> GetByDeviceId(Guid deviceId)
    {
        return await _database.Cpus
            .Where(cpu => cpu.DeviceId == deviceId)
            .ToListAsync();
    }

    public async Task<List<CpuDBO>> GetByDeviceIdWithMetrics(Guid deviceId)
    {
        return await _database.Cpus
            .Include(cpu => cpu.CpuMetrics)
            .Where(cpu => cpu.DeviceId == deviceId)
            .ToListAsync();
    }

    public async Task<List<CpuDBO>> GetByDeviceIdWithMetrics(Guid deviceId, DateTime from, DateTime to)
    {
        return await _database.Cpus
            .Include(cpu => cpu.CpuMetrics)
            .Where(cpu => cpu.DeviceId == deviceId)
            .Where(cpu => cpu.CpuMetrics.Any(metric => metric.Timestamp >= from && metric.Timestamp <= to))
            .ToListAsync();
    }
}