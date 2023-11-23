using Netmon.Data.DBO.Component.Interface;
using Netmon.Data.Repositories.Write.Component;
using Netmon.Models.Component.Interface.Metric;

namespace Netmon.Data.Services.Write.Component.Interface;

public interface IInterfaceMetricsWriteService : IComponentMetricWriteService<InterfaceMetric>
{ 
}