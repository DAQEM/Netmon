using Netmon.Models.Component;

namespace Netmon.Data.Repositories.Write.Component;

public interface IComponentWriteRepository<in T> : IWriteRepository where T : IComponent
{
    public Task AddOrUpdate(T component);
}