﻿using Netmon.Data.Services.Read.Component.Cpu;
using Netmon.Models.Component.Cpu;

namespace Netmon.Data.Services.Read.Services.Component.Cpu;

public class CpuReadService : ICpuReadService
{
    
    public Task<List<ICpu>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<ICpu?> GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<List<ICpu>> GetByDeviceId(Guid deviceId)
    {
        throw new NotImplementedException();
    }

    public Task<List<ICpu>> GetByDeviceIdWithMetrics(Guid deviceId)
    {
        throw new NotImplementedException();
    }

    public Task<List<ICpu>> GetByDeviceIdWithMetrics(Guid deviceId, DateTime from, DateTime to)
    {
        throw new NotImplementedException();
    }
}