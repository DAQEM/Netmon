using Netmon.Data.EntityFramework.Database;
using Netmon.Data.EntityFramework.DBO.Component.Interface;
using Netmon.Data.Repositories.Write.Component.Interface;
using Netmon.Models.Component.Interface.Metric;

namespace Netmon.Data.Write.Repositories.Component.Interface;

public class InterfaceMetricsWriteRepository : IInterfaceMetricsWriteRepository
{
    private readonly DevicesDatabase _database;
    
    public InterfaceMetricsWriteRepository(DevicesDatabase database)
    {
        _database = database;
    }

    public async Task Add(IInterfaceMetric interfaceMetrics)
    {
        if (interfaceMetrics == null)
        {
            throw new ArgumentNullException(nameof(interfaceMetrics));
        }
        
        InterfaceMetricsDBO interfaceMetricsDBO = InterfaceMetricsDBO.FromInterfaceMetric(interfaceMetrics);
        
        await _database.InterfaceMetrics.AddAsync(interfaceMetricsDBO);
    }
    
    public async Task SaveChanges()
    {
        await _database.SaveChangesAsync();
    }
}