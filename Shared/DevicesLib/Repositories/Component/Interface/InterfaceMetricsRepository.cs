using DevicesLib.Database;
using DevicesLib.DBO.Component.Interface;

namespace DevicesLib.Repositories.Component.Interface;

public class InterfaceMetricsRepository : IInterfaceMetricsRepository
{
    private readonly DevicesDatabase _database;
    
    public InterfaceMetricsRepository(DevicesDatabase database)
    {
        _database = database;
    }

    public async Task Add(InterfaceMetricsDBO interfaceMetrics)
    {
        if (interfaceMetrics == null)
        {
            throw new ArgumentNullException(nameof(interfaceMetrics));
        }
        
        await _database.InterfaceMetrics.AddAsync(interfaceMetrics);
        await _database.SaveChangesAsync();
    }
}