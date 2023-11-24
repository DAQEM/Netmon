using Microsoft.EntityFrameworkCore;
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

    public async Task<List<ICpu>> GetAll()
    {
        return await _database.Cpus
            .Select(dbo => dbo.ToCpu())
            .ToListAsync();
    }

    public async Task<ICpu?> GetById(Guid id)
    {
        return (await _database.Cpus
            .FirstOrDefaultAsync(device => device.Id == id))?
            .ToCpu();
    }

    public async Task<List<ICpu>> GetByDeviceId(Guid deviceId)
    {
        return await _database.Cpus
            .Where(cpu => cpu.DeviceId == deviceId)
            .Select(dbo => dbo.ToCpu())
            .ToListAsync();
    }

    public async Task<List<ICpu>> GetByDeviceIdWithMetrics(Guid deviceId)
    {
        return await _database.Cpus
            .Include(cpu => cpu.CpuMetrics)
            .Where(cpu => cpu.DeviceId == deviceId)
            .Select(dbo => dbo.ToCpu())
            .ToListAsync();
    }

    public async Task<List<ICpu>> GetByDeviceIdWithMetrics(Guid deviceId, DateTime from, DateTime to)
    {
        return await _database.Cpus
            .Include(cpu => cpu.CpuMetrics)
            .Where(cpu => cpu.DeviceId == deviceId)
            .Where(cpu => cpu.CpuMetrics.Any(metric => metric.Timestamp >= from && metric.Timestamp <= to))
            .Select(dbo => dbo.ToCpu())
            .ToListAsync();
    }
}