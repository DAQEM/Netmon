using DevicesLib.Database;
using DevicesLib.DBO.Component.Memory;
using Microsoft.EntityFrameworkCore;

namespace DevicesLib.Repositories.Component.Memory;

public class MemoryRepository : IMemoryRepository
{
    private readonly DevicesDatabase _database;
    
    private readonly IMemoryMetricsRepository _memoryMetricsRepository;

    public MemoryRepository(DevicesDatabase database, IMemoryMetricsRepository memoryMetricsRepository)
    {
        _database = database;
        _memoryMetricsRepository = memoryMetricsRepository;
    }

    public async Task AddOrUpdateMemory(MemoryDBO memory)
    {
        if (memory == null)
        {
            throw new ArgumentNullException(nameof(memory));
        }
        
        MemoryDBO? existingMemory = await _database.Memory.FirstOrDefaultAsync(m => m.Id == memory.Id);

        if (existingMemory == null)
        {
            _database.Entry(memory).CurrentValues.SetValues(memory);
        }
        else
        {
            await _database.Memory.AddAsync(memory);
        }
        
        await _database.SaveChangesAsync();
        
        if (memory.MemoryMetrics != null!)
        {
            foreach (MemoryMetricsDBO memoryMetrics in memory.MemoryMetrics)
            {
                await _memoryMetricsRepository.Add(memoryMetrics);
            }
        }
    }
}