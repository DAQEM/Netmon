using Microsoft.EntityFrameworkCore;
using Netmon.Data.DBO.Component.Memory;
using Netmon.Data.EntityFramework.Database;
using Netmon.Data.Repositories.Read.Component.Memory;

namespace Netmon.Data.EntityFramework.Read.Repositories.Component.Memory;

public class MemoryReadRepository : IMemoryReadRepository
{
    private readonly DevicesDatabase _database;
    
    private readonly IMemoryMetricReadRepository _memoryMetricsReadRepository;

    public MemoryReadRepository(DevicesDatabase database, IMemoryMetricReadRepository memoryMetricsReadRepository)
    {
        _database = database;
        _memoryMetricsReadRepository = memoryMetricsReadRepository;
    }

    public async Task<List<MemoryDBO>> GetAll()
    {
        return await _database.Memory.ToListAsync();
    }
}