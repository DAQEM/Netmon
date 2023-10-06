using Netmon.Data.DBO.Component;

namespace Netmon.Data.Repositories.Write.Component;

public interface IComponentWriteRepository<in T> : IWriteRepository where T : IComponentDBO
{
    public Task AddOrUpdate(T component);
}