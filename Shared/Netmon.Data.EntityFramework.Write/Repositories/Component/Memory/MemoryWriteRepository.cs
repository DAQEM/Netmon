using Microsoft.EntityFrameworkCore;
using Netmon.Data.DBO.Component.Memory;
using Netmon.Data.EntityFramework.Database;
using Netmon.Data.Repositories.Write.Component.Memory;

namespace Netmon.Data.Write.Repositories.Component.Memory;

public class MemoryWriteRepository(DevicesDatabase database, IMemoryMetricsWriteRepository memoryMetricsWriteRepository)
    : IMemoryWriteRepository
{
    public async Task AddOrUpdate(MemoryDBO memory)
    {
        if (memory is null)
        {
            throw new ArgumentNullException(nameof(memory));
        }
        
        MemoryDBO? existingMemory = await database.Memory.FirstOrDefaultAsync(m => m.DeviceId == memory.DeviceId && m.Index == memory.Index);

        if (existingMemory is null)
        {
            memory.Id = Guid.NewGuid();
            await database.Memory.AddAsync(memory);
        }
        else
        {
            memory.Id = existingMemory.Id;
            database.Entry(existingMemory).CurrentValues.SetValues(memory);
            
            if (memory.MemoryMetrics is not null)
            {
                foreach (MemoryMetricsDBO memoryMetrics in memory.MemoryMetrics)
                {
                    memoryMetrics.MemoryId = memory.Id;
                    await memoryMetricsWriteRepository.Add(memoryMetrics);
                }
            }
        }
    }
    
    public async Task SaveChanges()
    {
        await database.SaveChangesAsync();
    }
}