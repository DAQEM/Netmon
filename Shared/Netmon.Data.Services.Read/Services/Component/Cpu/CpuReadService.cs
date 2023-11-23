using Netmon.Data.Services.Read.Component.Cpu;

namespace Netmon.Data.Services.Read.Services.Component.Cpu;

public class CpuReadService : ICpuReadService
{
    
    public async Task<List<Models.Component.Cpu.Cpu>> GetAll()
    {
        throw new NotImplementedException();
    }

    public async Task<Models.Component.Cpu.Cpu?> GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Models.Component.Cpu.Cpu>> GetByDeviceId(Guid deviceId)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Models.Component.Cpu.Cpu>> GetByDeviceIdWithMetrics(Guid deviceId)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Models.Component.Cpu.Cpu>> GetByDeviceIdWithMetrics(Guid deviceId, DateTime from, DateTime to)
    {
        throw new NotImplementedException();
    }
}