using Netmon.Data.DBO.Component;

namespace Netmon.Data.Repositories.Write.Component;

public interface IComponentMetricWriteRepository<in T> : IWriteRepository where T : IComponentMetricDBO
{
    Task Add(T metric);
}