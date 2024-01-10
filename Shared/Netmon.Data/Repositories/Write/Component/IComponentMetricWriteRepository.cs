using Netmon.Data.DBO.Component;
using Netmon.Models.Component;

namespace Netmon.Data.Repositories.Write.Component;

public interface IComponentMetricWriteRepository<in T> : IWriteRepository where T : IComponentMetricDBO
{
    Task Add(T metric);
}