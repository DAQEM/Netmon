using Netmon.Data.Services.Read.Component.Interface;
using Netmon.Models.Component.Interface.Metric;

namespace Netmon.Data.Services.Read.Services.Component.Interface;

public class InterfaceMetricsReadService : IInterfaceMetricReadService
{
    
    public async Task<List<InterfaceMetric>> GetAll()
    {
        throw new NotImplementedException();
    }

    public async Task<InterfaceMetric?> GetById(Guid id)
    {
        throw new NotImplementedException();
    }
    
    public async Task<List<InterfaceMetric>> GetByComponentId(Guid componentId)
    {
        throw new NotImplementedException();
    }

    public async Task<List<InterfaceMetric>> GetByComponentIds(List<Guid> componentIds)
    {
        throw new NotImplementedException();
    }
}