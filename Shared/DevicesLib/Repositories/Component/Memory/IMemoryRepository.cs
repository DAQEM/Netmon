using DevicesLib.DBO.Component.Memory;

namespace DevicesLib.Repositories.Component.Memory;

public interface IMemoryRepository
{
    Task AddOrUpdateMemory(MemoryDBO memory);
    
    Task SaveChanges();
}