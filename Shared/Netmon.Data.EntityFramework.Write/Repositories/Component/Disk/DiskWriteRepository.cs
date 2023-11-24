using Microsoft.EntityFrameworkCore;
using Netmon.Data.EntityFramework.Database;
using Netmon.Data.EntityFramework.DBO.Component.Disk;
using Netmon.Data.Repositories.Write.Component.Disk;
using Netmon.Models.Component.Disk;

namespace Netmon.Data.Write.Repositories.Component.Disk;

public class DiskWriteRepository : IDiskWriteRepository
{
    private readonly DevicesDatabase _database;
    
    private readonly IDiskMetricsWriteRepository _diskMetricsWriteRepository;

    public DiskWriteRepository(DevicesDatabase database, IDiskMetricsWriteRepository diskMetricsWriteRepository)
    {
        _database = database;
        _diskMetricsWriteRepository = diskMetricsWriteRepository;
    }

    public async Task AddOrUpdate(IDisk disk)
    {
        if (disk == null)
        {
            throw new ArgumentNullException(nameof(disk));
        }
        
        DiskDBO diskDBO = DiskDBO.FromDisk(disk);
        
        DiskDBO? existingDisk = await _database.Disks.FirstOrDefaultAsync(d => d.DeviceId == diskDBO.DeviceId && d.Index == diskDBO.Index);

        if (existingDisk == null)
        {
            diskDBO.Id = Guid.NewGuid();
            await _database.Disks.AddAsync(diskDBO);
        }
        else
        {
            diskDBO.Id = existingDisk.Id;
            _database.Entry(existingDisk).CurrentValues.SetValues(diskDBO);
            
            if (diskDBO.DiskMetrics != null!)
            {
                foreach (DiskMetricsDBO diskMetrics in diskDBO.DiskMetrics)
                {
                    diskMetrics.DiskId = diskDBO.Id;
                    await _diskMetricsWriteRepository.Add(diskMetrics.ToDiskMetric());
                }
            }
        }
    }
    
    public async Task SaveChanges()
    {
        await _database.SaveChangesAsync();
    }
}