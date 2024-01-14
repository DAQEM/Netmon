using Microsoft.EntityFrameworkCore;
using Netmon.Data.DBO.Component.Interface;
using Netmon.Data.EntityFramework.Database;
using Netmon.Data.Repositories.Write.Component.Interface;

namespace Netmon.Data.Write.Repositories.Component.Interface;

public class InterfaceWriteRepository(
    DevicesDatabase database,
    IInterfaceMetricsWriteRepository interfaceMetricsWriteRepository)
    : IInterfaceWriteRepository
{
    public async Task AddOrUpdate(InterfaceDBO @interface)
    {
        if (@interface == null)
        {
            throw new ArgumentNullException(nameof(@interface));
        }
        
        InterfaceDBO? existingInterface = await database.Interfaces.FirstOrDefaultAsync(i => i.DeviceId == @interface.DeviceId && i.Index == @interface.Index);
        
        if (existingInterface == null)
        {
            @interface.Id = Guid.NewGuid();
            await database.Interfaces.AddAsync(@interface);
        }
        else
        {
            @interface.Id = existingInterface.Id;
            database.Entry(existingInterface).CurrentValues.SetValues(@interface);
            
            if (@interface.InterfaceMetrics != null!)
            {
                foreach (InterfaceMetricsDBO interfaceMetrics in @interface.InterfaceMetrics)
                {
                    interfaceMetrics.InterfaceId = @interface.Id;
                    await interfaceMetricsWriteRepository.Add(interfaceMetrics);
                }
            }
        }
    }
    
    public async Task SaveChanges()
    {
        await database.SaveChangesAsync();
    }
}