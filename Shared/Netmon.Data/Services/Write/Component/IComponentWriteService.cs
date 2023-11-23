using Netmon.Data.DBO.Component;
using Netmon.Data.Repositories.Write;
using Netmon.Models.Component;

namespace Netmon.Data.Services.Write.Component;

public interface IComponentWriteService<in T> : IWriteService where T : IComponent
{
    public Task AddOrUpdate(T component);
}