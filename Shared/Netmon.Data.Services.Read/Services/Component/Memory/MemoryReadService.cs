using Netmon.Data.Services.Read.Component.Memory;
using Netmon.Models.Component.Memory;

namespace Netmon.Data.Services.Read.Services.Component.Memory;

public class MemoryReadService : IMemoryReadService
{

    public async Task<List<IMemory>> GetAll()
    {
        throw new NotImplementedException();
    }

    public async Task<IMemory?> GetById(Guid id)
    {
        throw new NotImplementedException();
    }
    
    public async Task<List<IMemory>> GetByDeviceId(Guid deviceId)
    {
        throw new NotImplementedException();
    }

    public async Task<List<IMemory>> GetByDeviceIdWithMetrics(Guid deviceId)
    {
        throw new NotImplementedException();
    }
    
    public async Task<List<IMemory>> GetByDeviceIdWithMetrics(Guid deviceId, DateTime from, DateTime to)
    {
        throw new NotImplementedException();
    }
}