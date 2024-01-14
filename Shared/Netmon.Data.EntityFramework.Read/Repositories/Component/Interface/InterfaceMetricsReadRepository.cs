using Microsoft.EntityFrameworkCore;
using Netmon.Data.DBO.Component.Interface;
using Netmon.Data.EntityFramework.Database;
using Netmon.Data.Repositories.Read.Component.Interface;

namespace Netmon.Data.EntityFramework.Read.Repositories.Component.Interface;

public class InterfaceMetricsReadRepository(DevicesDatabase database) : IInterfaceMetricReadRepository
{
    public async Task<List<InterfaceMetricsDBO>> GetAll()
    {
        return await database.InterfaceMetrics.ToListAsync();
    }

    public async Task<InterfaceMetricsDBO?> GetById(Guid id)
    {
        return await database.InterfaceMetrics.FirstOrDefaultAsync(device => device.Id == id);
    }
    
    public async Task<List<InterfaceMetricsDBO>> GetByComponentId(Guid componentId)
    {
        return await database.InterfaceMetrics.Where(@interface => @interface.InterfaceId == componentId).ToListAsync();
    }

    public async Task<List<InterfaceMetricsDBO>> GetByComponentIds(List<Guid> componentIds)
    {
        return await database.InterfaceMetrics.Where(@interface => componentIds.Contains(@interface.InterfaceId)).ToListAsync();
    }
}