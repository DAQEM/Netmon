using DevicesLib.Database;
using DevicesLib.DBO.Component.Interface;
using Microsoft.EntityFrameworkCore;

namespace DevicesLib.Repositories.Component.Interface;

public class InterfaceRepository : IInterfaceRepository
{
    private readonly DevicesDatabase _database;
    
    private readonly IInterfaceMetricsRepository _interfaceMetricsRepository;

    public InterfaceRepository(DevicesDatabase database, IInterfaceMetricsRepository interfaceMetricsRepository)
    {
        _database = database;
        _interfaceMetricsRepository = interfaceMetricsRepository;
    }

    public async Task AddOrUpdateInterface(InterfaceDBO @interface)
    {
        if (@interface == null)
        {
            throw new ArgumentNullException(nameof(@interface));
        }
        
        InterfaceDBO? existingInterface = await _database.Interfaces.FirstOrDefaultAsync(i => i.Id == @interface.Id);
        
        if (existingInterface == null)
        {
            _database.Entry(@interface).CurrentValues.SetValues(@interface);
        }
        else
        {
            await _database.Interfaces.AddAsync(@interface);
        }
        
        await _database.SaveChangesAsync();
        
        if (@interface.InterfaceMetrics != null!)
        {
            foreach (InterfaceMetricsDBO interfaceMetrics in @interface.InterfaceMetrics)
            {
                await _interfaceMetricsRepository.Add(interfaceMetrics);
            }
        }
    }
}