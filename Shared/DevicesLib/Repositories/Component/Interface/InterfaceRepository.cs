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
        
        InterfaceDBO? existingInterface = await _database.Interfaces.FirstOrDefaultAsync(i => i.DeviceId == @interface.DeviceId && i.Index == @interface.Index);
        
        if (existingInterface == null)
        {
            @interface.Id = Guid.NewGuid();
            await _database.Interfaces.AddAsync(@interface);
        }
        else
        {
            @interface.Id = existingInterface.Id;
            _database.Entry(existingInterface).CurrentValues.SetValues(@interface);
            
            if (@interface.InterfaceMetrics != null!)
            {
                foreach (InterfaceMetricsDBO interfaceMetrics in @interface.InterfaceMetrics)
                {
                    interfaceMetrics.InterfaceId = @interface.Id;
                    await _interfaceMetricsRepository.Add(interfaceMetrics);
                }
            }
        }
    }
    
    public async Task SaveChanges()
    {
        await _database.SaveChangesAsync();
    }
}