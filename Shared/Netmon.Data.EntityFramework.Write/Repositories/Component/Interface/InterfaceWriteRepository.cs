using Microsoft.EntityFrameworkCore;
using Netmon.Data.EntityFramework.Database;
using Netmon.Data.EntityFramework.DBO.Component.Interface;
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

    public async Task AddOrUpdate(IInterface @interface)
    {
        if (@interface == null)
        {
            throw new ArgumentNullException(nameof(@interface));
        }
        
        InterfaceDBO interfaceDBO = InterfaceDBO.FromInterface(@interface);
        
        InterfaceDBO? existingInterface = await _database.Interfaces.FirstOrDefaultAsync(i => i.DeviceId == interfaceDBO.DeviceId && i.Index == interfaceDBO.Index);
        
        if (existingInterface == null)
        {
            interfaceDBO.Id = Guid.NewGuid();
            await _database.Interfaces.AddAsync(interfaceDBO);
        }
        else
        {
            interfaceDBO.Id = existingInterface.Id;
            _database.Entry(existingInterface).CurrentValues.SetValues(interfaceDBO);
            
            if (interfaceDBO.InterfaceMetrics != null!)
            {
                foreach (InterfaceMetricsDBO interfaceMetrics in interfaceDBO.InterfaceMetrics)
                {
                    interfaceMetrics.InterfaceId = interfaceDBO.Id;
                    await _interfaceMetricsWriteRepository.Add(interfaceMetrics.ToInterfaceMetric());
                }
            }
        }
    }
    
    public async Task SaveChanges()
    {
        await _database.SaveChangesAsync();
    }
}