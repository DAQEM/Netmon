using Microsoft.EntityFrameworkCore;
using Netmon.Data.DBO.Component.Memory;
using Netmon.Data.EntityFramework.Database;
using Netmon.Data.Repositories.Write.Component.Memory;
using Netmon.Models.Component.Memory;

namespace Netmon.Data.Write.Repositories.Component.Memory;

public class MemoryWriteRepository : IMemoryWriteRepository
{
    private readonly DevicesDatabase _database;
    
    private readonly IMemoryMetricsWriteRepository _memoryMetricsWriteRepository;

    public MemoryWriteRepository(DevicesDatabase database, IMemoryMetricsWriteRepository memoryMetricsWriteRepository)
    {
        _database = database;
        _memoryMetricsWriteRepository = memoryMetricsWriteRepository;
    }

    public async Task AddOrUpdate(MemoryDBO memory)
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
                    await _memoryMetricsWriteRepository.Add(memoryMetrics);
                }
            }
        }
    }
    
    public async Task SaveChanges()
    {
        await _database.SaveChangesAsync();
    }
}