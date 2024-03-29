﻿using Netmon.Data.DBO.Component.Memory;
using Netmon.Data.EntityFramework.Database;
using Netmon.Data.Repositories.Write.Component.Memory;

namespace Netmon.Data.Write.Repositories.Component.Memory;

public class MemoryMetricsWriteRepository(DevicesDatabase database) : IMemoryMetricsWriteRepository
{
    public async Task Add(MemoryMetricsDBO memoryMetrics)
    {
        if (memoryMetrics is null)
        {
            throw new ArgumentNullException(nameof(memoryMetrics));
        }
        
        await database.MemoryMetrics.AddAsync(memoryMetrics);
    }
    
    public async Task SaveChanges()
    {
        await database.SaveChangesAsync();
    }
}