using Netmon.Data.DBO.Component;
using Netmon.Data.Repositories.Write;
using Netmon.Models.Component;

namespace Netmon.Data.Services.Write.Component;

public interface IComponentMetricWriteService<in T> : IWriteService where T : IComponentMetric
{
    Task Add(T metric);
}