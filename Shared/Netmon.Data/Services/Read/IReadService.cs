using Netmon.Data.DBO;
using Netmon.Models;

namespace Netmon.Data.Services.Read;

public interface IReadService<T> where T : IModel
{
    Task<List<T>> GetAll();
    
    Task<T?> GetById(Guid id);
}