using Netmon.Data.Services.Read.Component.Interface;

namespace Netmon.Data.Services.Read.Services.Component.Interface;

public class InterfaceReadService : IInterfaceReadService
{

    public async Task<List<Models.Component.Interface.Interface>> GetAll()
    {
        throw new NotImplementedException();
    }

    public async Task<Models.Component.Interface.Interface?> GetById(Guid id)
    {
        throw new NotImplementedException();
    }
    
    public async Task<List<Models.Component.Interface.Interface>> GetByDeviceId(Guid deviceId)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Models.Component.Interface.Interface>> GetByDeviceIdWithMetrics(Guid deviceId)
    {
        throw new NotImplementedException();
    }
    
    public async Task<List<Models.Component.Interface.Interface>> GetByDeviceIdWithMetrics(Guid deviceId, DateTime from, DateTime to)
    {
        throw new NotImplementedException();
    }
}