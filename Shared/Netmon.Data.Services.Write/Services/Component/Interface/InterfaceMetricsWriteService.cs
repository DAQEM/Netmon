using Netmon.Data.Services.Write.Component.Interface;
using Netmon.Models.Component.Interface.Metric;

namespace Netmon.Data.Services.Write.Services.Component.Interface;

public class InterfaceMetricsWriteService : IInterfaceMetricsWriteService
{
    public Task Add(IInterfaceMetric metric)
    {
        throw new NotImplementedException();
    }

    public Task SaveChanges()
    {
        throw new NotImplementedException();
    }
}