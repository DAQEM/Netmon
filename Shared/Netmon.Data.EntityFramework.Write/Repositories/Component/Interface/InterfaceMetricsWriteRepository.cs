using Netmon.Data.DBO.Component.Interface;
using Netmon.Data.EntityFramework.Database;
using Netmon.Data.Repositories.Write.Component.Interface;

namespace Netmon.Data.Write.Repositories.Component.Interface;

public class InterfaceMetricsWriteRepository(DevicesDatabase database) : IInterfaceMetricsWriteRepository
{
    public async Task Add(InterfaceMetricsDBO interfaceMetrics)
    {
        if (interfaceMetrics == null)
        {
            throw new ArgumentNullException(nameof(interfaceMetrics));
        }
        
        await database.InterfaceMetrics.AddAsync(interfaceMetrics);
    }
    
    public async Task SaveChanges()
    {
        await database.SaveChangesAsync();
    }
}