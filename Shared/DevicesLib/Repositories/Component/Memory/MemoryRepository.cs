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
        
        MemoryDBO? existingMemory = await _database.Memory.FirstOrDefaultAsync(m => m.DeviceId == memory.DeviceId && m.Index == memory.Index);

        if (existingMemory == null)
        {
            memory.Id = Guid.NewGuid();
            await _database.Memory.AddAsync(memory);
        }
        else
        {
            memory.Id = existingMemory.Id;
            _database.Entry(existingMemory).CurrentValues.SetValues(memory);
            
            if (memory.MemoryMetrics != null!)
            {
                foreach (MemoryMetricsDBO memoryMetrics in memory.MemoryMetrics)
                {
                    memoryMetrics.MemoryId = memory.Id;
                    await _memoryMetricsRepository.Add(memoryMetrics);
                }
            }
        }
    }
    
    public async Task SaveChanges()
    {
        await _database.SaveChangesAsync();
    }
}