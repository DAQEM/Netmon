using Netmon.Data.Services.Read.Component.Interface;
using Netmon.Models.Component.Interface.Metric;

namespace Netmon.Data.Services.Read.Services.Component.Interface;

public class InterfaceMetricsReadService : IInterfaceMetricReadService
{
    
    public Task<List<IInterfaceMetric>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<IInterfaceMetric?> GetById(Guid id)
    {
        throw new NotImplementedException();
    }
    
    public Task<List<IInterfaceMetric>> GetByComponentId(Guid componentId)
    {
        throw new NotImplementedException();
    }

    public Task<List<IInterfaceMetric>> GetByComponentIds(List<Guid> componentIds)
    {
        throw new NotImplementedException();
    }
}