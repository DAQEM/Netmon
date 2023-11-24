using Netmon.Data.Services.Read.Component.Interface;
using Netmon.Models.Component.Interface;

namespace Netmon.Data.Services.Read.Services.Component.Interface;

public class InterfaceReadService : IInterfaceReadService
{

    public async Task<List<IInterface>> GetAll()
    {
        throw new NotImplementedException();
    }

    public async Task<IInterface?> GetById(Guid id)
    {
        throw new NotImplementedException();
    }
    
    public async Task<List<IInterface>> GetByDeviceId(Guid deviceId)
    {
        throw new NotImplementedException();
    }

    public async Task<List<IInterface>> GetByDeviceIdWithMetrics(Guid deviceId)
    {
        throw new NotImplementedException();
    }
    
    public async Task<List<IInterface>> GetByDeviceIdWithMetrics(Guid deviceId, DateTime from, DateTime to)
    {
        throw new NotImplementedException();
    }
}