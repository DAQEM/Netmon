using Microsoft.EntityFrameworkCore;
using Netmon.Data.DBO.Component.Disk;
using Netmon.Data.EntityFramework.Database;
using Netmon.Data.Repositories.Read.Component.Disk;

namespace Netmon.Data.EntityFramework.Read.Repositories.Component.Disk;

public class DiskReadRepository : IDiskReadRepository
{
    private readonly DevicesDatabase _database;
    
    private readonly IDiskMetricReadRepository _diskMetricsReadRepository;

    public DiskReadRepository(DevicesDatabase database, IDiskMetricReadRepository diskMetricsReadRepository)
    {
        _database = database;
        _diskMetricsReadRepository = diskMetricsReadRepository;
    }

    public async Task<List<DiskDBO>> GetAll()
    {
        return await _database.Disks.ToListAsync();
    }
}