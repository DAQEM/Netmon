using Microsoft.EntityFrameworkCore;
using Netmon.Data.EntityFramework.Database;
using Netmon.Data.EntityFramework.DBO.Component.Memory;
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

    public async Task AddOrUpdate(IMemory memory)
    {
        if (memory == null)
        {
            throw new ArgumentNullException(nameof(memory));
        }
        
        MemoryDBO memoryDBO = MemoryDBO.FromMemory(memory);
        
        MemoryDBO? existingMemory = await _database.Memory.FirstOrDefaultAsync(m => m.DeviceId == memoryDBO.DeviceId && m.Index == memoryDBO.Index);

        if (existingMemory == null)
        {
            memoryDBO.Id = Guid.NewGuid();
            await _database.Memory.AddAsync(memoryDBO);
        }
        else
        {
            memoryDBO.Id = existingMemory.Id;
            _database.Entry(existingMemory).CurrentValues.SetValues(memoryDBO);
            
            if (memoryDBO.MemoryMetrics != null!)
            {
                foreach (MemoryMetricsDBO memoryMetrics in memoryDBO.MemoryMetrics)
                {
                    memoryMetrics.MemoryId = memoryDBO.Id;
                    await _memoryMetricsWriteRepository.Add(memoryMetrics.ToMemoryMetric());
                }
            }
        }
    }
    
    public async Task SaveChanges()
    {
        await _database.SaveChangesAsync();
    }
}