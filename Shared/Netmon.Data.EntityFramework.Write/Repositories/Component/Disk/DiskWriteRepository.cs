using Microsoft.EntityFrameworkCore;
using Netmon.Data.DBO.Component.Disk;
using Netmon.Data.EntityFramework.Database;
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

    public async Task AddOrUpdate(DiskDBO disk)
    {
        if (disk == null)
        {
            throw new ArgumentNullException(nameof(disk));
        }
        
        DiskDBO? existingDisk = await _database.Disks.FirstOrDefaultAsync(d => d.DeviceId == disk.DeviceId && d.Index == disk.Index);

        if (existingDisk == null)
        {
            disk.Id = Guid.NewGuid();
            await _database.Disks.AddAsync(disk);
        }
        else
        {
            disk.Id = existingDisk.Id;
            _database.Entry(existingDisk).CurrentValues.SetValues(disk);
            
            if (disk.DiskMetrics != null!)
            {
                foreach (DiskMetricsDBO diskMetrics in disk.DiskMetrics)
                {
                    diskMetrics.DiskId = disk.Id;
                    await _diskMetricsWriteRepository.Add(diskMetrics);
                }
            }
        }
    }
    
    public async Task SaveChanges()
    {
        await _database.SaveChangesAsync();
    }
}