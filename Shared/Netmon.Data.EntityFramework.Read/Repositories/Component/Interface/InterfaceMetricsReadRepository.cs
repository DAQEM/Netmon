using Microsoft.EntityFrameworkCore;
using Netmon.Data.DBO.Component.Interface;
using Netmon.Data.DBO.Device;
using Netmon.Data.EntityFramework.Database;
using Netmon.Data.Repositories.Read.Component.Interface;

namespace Netmon.Data.EntityFramework.Read.Repositories.Component.Interface;

public class InterfaceMetricsReadRepository : IInterfaceMetricReadRepository
{
    private readonly DevicesDatabase _database;
    
    public InterfaceMetricsReadRepository(DevicesDatabase database)
    {
        _database = database;
    }

    public async Task<List<InterfaceMetricsDBO>> GetAll()
    {
        return await _database.InterfaceMetrics.ToListAsync();
    }

    public async Task<InterfaceMetricsDBO?> GetById(Guid id)
    {
        return await _database.InterfaceMetrics.FirstOrDefaultAsync(device => device.Id == id);
    }
}