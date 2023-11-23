using Netmon.Data.Services.Read.Component.Disk;

namespace Netmon.Data.Services.Read.Services.Component.Disk;

public class DiskReadService : IDiskReadService
{

    public async Task<List<Models.Component.Disk.Disk>> GetAll()
    {
        throw new NotImplementedException();
    }

    public async Task<Models.Component.Disk.Disk?> GetById(Guid id)
    {
        throw new NotImplementedException();
    }
    
    public async Task<List<Models.Component.Disk.Disk>> GetByDeviceId(Guid deviceId)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Models.Component.Disk.Disk>> GetByDeviceIdWithMetrics(Guid deviceId)
    {
        throw new NotImplementedException();
    }
    
    public async Task<List<Models.Component.Disk.Disk>> GetByDeviceIdWithMetrics(Guid deviceId, DateTime from, DateTime to)
    {
        throw new NotImplementedException();
    }
}