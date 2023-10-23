using Microsoft.EntityFrameworkCore;
using Netmon.Data.DBO.Component.Memory;
using Netmon.Data.DBO.Device;
using Netmon.Data.EntityFramework.Database;
using Netmon.Data.Repositories.Read.Component.Memory;

namespace Netmon.Data.EntityFramework.Read.Repositories.Component.Memory;

public class MemoryMetricsReadRepository : IMemoryMetricReadRepository
{
    private readonly DevicesDatabase _database;

    public MemoryMetricsReadRepository(DevicesDatabase database)
    {
        _database = database;
    }

    public async Task<List<MemoryMetricsDBO>> GetAll()
    {
        return await _database.MemoryMetrics.ToListAsync();
    }

    public async Task<MemoryMetricsDBO?> GetById(Guid id)
    {
        return await _database.MemoryMetrics.FirstOrDefaultAsync(device => device.Id == id);
    }
}