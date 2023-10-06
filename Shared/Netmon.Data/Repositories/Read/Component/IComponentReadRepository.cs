using Netmon.Data.DBO.Component;

namespace Netmon.Data.Repositories.Read.Component;

public interface IComponentReadRepository<T>: IReadRepository<T> where T: IComponentDBO
{
    
}