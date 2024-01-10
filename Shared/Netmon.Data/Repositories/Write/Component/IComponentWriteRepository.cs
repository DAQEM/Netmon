using Netmon.Data.DBO.Component;
using Netmon.Models.Component;

namespace Netmon.Data.Repositories.Write.Component;

public interface IComponentWriteRepository<in T> : IWriteRepository where T : IComponentDBO
{
    public Task AddOrUpdate(T component);
}