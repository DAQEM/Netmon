﻿using Netmon.Data.Repositories.Read.Component.Interface;
using Netmon.Data.Services.Read.Component.Interface;
using Netmon.Models.Component.Interface;

namespace Netmon.Data.Services.Read.Services.Component.Interface;

public class InterfaceReadService(IInterfaceReadRepository interfaceReadRepository) : IInterfaceReadService
{
    public Task<List<IInterface>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<IInterface?> GetById(Guid id)
    {
        throw new NotImplementedException();
    }
    
    public Task<List<IInterface>> GetByDeviceId(Guid deviceId)
    {
        throw new NotImplementedException();
    }

    public Task<List<IInterface>> GetByDeviceIdWithMetrics(Guid deviceId)
    {
        throw new NotImplementedException();
    }
    
    public async Task<List<IInterface>> GetByDeviceIdWithMetrics(Guid deviceId, DateTime from, DateTime to)
    {
        return (await interfaceReadRepository.GetByDeviceIdWithMetrics(deviceId, from, to))
            .Select(x => x.ToInterface())
            .ToList();
    }
}