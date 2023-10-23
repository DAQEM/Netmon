using Microsoft.EntityFrameworkCore;
using Netmon.Data.DBO.Component.Interface;
using Netmon.Data.DBO.Device;
using Netmon.Data.EntityFramework.Database;
using Netmon.Data.Repositories.Read.Component.Interface;

namespace Netmon.Data.EntityFramework.Read.Repositories.Component.Interface;

public class InterfaceReadRepository : IInterfaceReadRepository
{
    private readonly DevicesDatabase _database;
    
    private readonly IInterfaceMetricReadRepository _interfaceMetricsReadRepository;

    public InterfaceReadRepository(DevicesDatabase database, IInterfaceMetricReadRepository interfaceMetricsReadRepository)
    {
        _database = database;
        _interfaceMetricsReadRepository = interfaceMetricsReadRepository;
    }

    public async Task<List<InterfaceDBO>> GetAll()
    {
        return await _database.Interfaces.ToListAsync();
    }

    public async Task<InterfaceDBO?> GetById(Guid id)
    {
        return await _database.Interfaces.FirstOrDefaultAsync(device => device.Id == id);
    }
}