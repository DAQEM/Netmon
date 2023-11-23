using Netmon.Data.DBO.Component.Memory;
using Netmon.Data.Repositories.Read.Component.Memory;
using Netmon.Data.Services.Read.Component.Memory;

namespace Netmon.Data.Services.Read.Services.Component.Memory;

public class MemoryReadService : IMemoryReadService
{

    public async Task<List<Models.Component.Memory.Memory>> GetAll()
    {
        throw new NotImplementedException();
    }

    public async Task<Models.Component.Memory.Memory?> GetById(Guid id)
    {
        throw new NotImplementedException();
    }
    
    public async Task<List<Models.Component.Memory.Memory>> GetByDeviceId(Guid deviceId)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Models.Component.Memory.Memory>> GetByDeviceIdWithMetrics(Guid deviceId)
    {
        throw new NotImplementedException();
    }
    
    public async Task<List<Models.Component.Memory.Memory>> GetByDeviceIdWithMetrics(Guid deviceId, DateTime from, DateTime to)
    {
        throw new NotImplementedException();
    }
}