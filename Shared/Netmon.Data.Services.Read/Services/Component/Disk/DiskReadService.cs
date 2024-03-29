﻿using Netmon.Data.Repositories.Read.Component.Disk;
using Netmon.Data.Services.Read.Component.Disk;
using Netmon.Models.Component.Disk;

namespace Netmon.Data.Services.Read.Services.Component.Disk;

public class DiskReadService(IDiskReadRepository diskReadRepository) : IDiskReadService
{
    public Task<List<IDisk>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<IDisk?> GetById(Guid id)
    {
        throw new NotImplementedException();
    }
    
    public Task<List<IDisk>> GetByDeviceId(Guid deviceId)
    {
        throw new NotImplementedException();
    }

    public Task<List<IDisk>> GetByDeviceIdWithMetrics(Guid deviceId)
    {
        throw new NotImplementedException();
    }
    
    public async Task<List<IDisk>> GetByDeviceIdWithMetrics(Guid deviceId, DateTime from, DateTime to)
    {
        return (await diskReadRepository.GetByDeviceIdWithMetrics(deviceId, from, to))
            .Select(x => x.ToDisk())
            .ToList();
    }
}