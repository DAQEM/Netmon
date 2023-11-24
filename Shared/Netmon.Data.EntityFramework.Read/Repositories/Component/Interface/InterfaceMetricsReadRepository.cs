using Microsoft.EntityFrameworkCore;
using Netmon.Data.EntityFramework.Database;
using Netmon.Data.EntityFramework.DBO.Component.Interface;
using Netmon.Data.Repositories.Read.Component.Interface;
using Netmon.Models.Component.Interface.Metric;

namespace Netmon.Data.EntityFramework.Read.Repositories.Component.Interface;

public class InterfaceMetricsReadRepository : IInterfaceMetricReadRepository
{
    private readonly DevicesDatabase _database;
    
    public InterfaceMetricsReadRepository(DevicesDatabase database)
    {
        _database = database;
    }

    public async Task<List<IInterfaceMetric>> GetAll()
    {
        return await _database.InterfaceMetrics.Select(dbo => dbo.ToInterfaceMetric()).ToListAsync();
    }

    public async Task<IInterfaceMetric?> GetById(Guid id)
    {
        return (await _database.InterfaceMetrics.FirstOrDefaultAsync(device => device.Id == id))?.ToInterfaceMetric();
    }
    
    public async Task<List<IInterfaceMetric>> GetByComponentId(Guid componentId)
    {
        return await _database.InterfaceMetrics.Where(@interface => @interface.InterfaceId == componentId).Select(dbo => dbo.ToInterfaceMetric()).ToListAsync();
    }

    public async Task<List<IInterfaceMetric>> GetByComponentIds(List<Guid> componentIds)
    {
        return await _database.InterfaceMetrics.Where(@interface => componentIds.Contains(@interface.InterfaceId)).Select(dbo => dbo.ToInterfaceMetric()).ToListAsync();
    }
}