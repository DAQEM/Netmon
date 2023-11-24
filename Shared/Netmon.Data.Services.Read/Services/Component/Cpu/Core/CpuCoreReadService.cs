using Netmon.Data.Services.Read.Component.Cpu.Core;
using Netmon.Models.Component.Cpu.Core.Metric;

namespace Netmon.Data.Services.Read.Services.Component.Cpu.Core;

public class CpuCoreReadService : ICpuCoreReadService
{
    
    public async Task<List<ICpuCoreMetric>> GetAll()
    {
        throw new NotImplementedException();
    }

    public async Task<ICpuCoreMetric?> GetById(Guid id)
    {
        throw new NotImplementedException();
    }
}