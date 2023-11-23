using Netmon.Data.DBO.Component.Cpu.Core;
using Netmon.Data.Repositories.Read.Component.Cpu.Core;
using Netmon.Data.Services.Read.Component.Cpu.Core;
using Netmon.Models.Component.Cpu.Core.Metric;

namespace Netmon.Data.Services.Read.Services.Component.Cpu.Core;

public class CpuCoreReadService : ICpuCoreReadService
{
    
    public async Task<List<CpuCoreMetric>> GetAll()
    {
        throw new NotImplementedException();
    }

    public async Task<CpuCoreMetric?> GetById(Guid id)
    {
        throw new NotImplementedException();
    }
}