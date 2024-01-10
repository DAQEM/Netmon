using Microsoft.EntityFrameworkCore;
using Netmon.Data.DBO.Component.Interface;
using Netmon.Data.EntityFramework.Database;
using Netmon.Data.Repositories.Write.Component.Interface;
using Netmon.Models.Component.Interface;

namespace Netmon.Data.Write.Repositories.Component.Interface;

public class InterfaceWriteRepository : IInterfaceWriteRepository
{
    private readonly DevicesDatabase _database;
    
    private readonly IInterfaceMetricsWriteRepository _interfaceMetricsWriteRepository;

    public InterfaceWriteRepository(DevicesDatabase database, IInterfaceMetricsWriteRepository interfaceMetricsWriteRepository)
    {
        _database = database;
        _interfaceMetricsWriteRepository = interfaceMetricsWriteRepository;
    }

    public async Task AddOrUpdate(InterfaceDBO @interface)
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
                    await _interfaceMetricsWriteRepository.Add(interfaceMetrics);
                }
            }
        }
    }
    
    public async Task SaveChanges()
    {
        await _database.SaveChangesAsync();
    }
}