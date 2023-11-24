using Netmon.Models.Component;

namespace Netmon.Data.Repositories.Write.Component;

public interface IComponentMetricWriteRepository<in T> : IWriteRepository where T : IComponentMetric
{
    Task Add(T metric);
}