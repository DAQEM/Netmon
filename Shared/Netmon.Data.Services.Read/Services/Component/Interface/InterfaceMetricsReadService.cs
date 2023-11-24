using Netmon.Data.Services.Read.Component.Interface;
using Netmon.Models.Component.Interface.Metric;

namespace Netmon.Data.Services.Read.Services.Component.Interface;

public class InterfaceMetricsReadService : IInterfaceMetricReadService
{
    
    public async Task<List<IInterfaceMetric>> GetAll()
    {
        throw new NotImplementedException();
    }

    public async Task<IInterfaceMetric?> GetById(Guid id)
    {
        throw new NotImplementedException();
    }
    
    public async Task<List<IInterfaceMetric>> GetByComponentId(Guid componentId)
    {
        throw new NotImplementedException();
    }

    public async Task<List<IInterfaceMetric>> GetByComponentIds(List<Guid> componentIds)
    {
        throw new NotImplementedException();
    }
}