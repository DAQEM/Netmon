using DevicesLib.Database;
using DevicesLib.DBO.Component.Disk;
using Microsoft.EntityFrameworkCore;

namespace DevicesLib.Repositories.Component.Disk;

public class DiskRepository : IDiskRepository
{
    private readonly DevicesDatabase _database;
    
    private readonly IDiskMetricsRepository _diskMetricsRepository;

    public DiskRepository(DevicesDatabase database, IDiskMetricsRepository diskMetricsRepository)
    {
        _database = database;
        _diskMetricsRepository = diskMetricsRepository;
    }

    public async Task AddOrUpdateDisk(DiskDBO disk)
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
                    await _diskMetricsRepository.Add(diskMetrics);
                }
            }
        }
    }
    
    public async Task SaveChanges()
    {
        await _database.SaveChangesAsync();
    }
}