using Netmon.Data.DBO;
using Netmon.Models;

namespace Netmon.Data.Repositories.Read;

public interface IReadRepository<T> where T : IDBO
{
    Task<List<T>> GetAll();
    
    Task<T?> GetById(Guid id);
}