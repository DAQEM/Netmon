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
        
        DiskDBO? existingDisk = await _database.Disks.FirstOrDefaultAsync(d => d.Id == disk.Id);

        if (existingDisk == null)
        {
            _database.Entry(disk).CurrentValues.SetValues(disk);
        }
        else
        {
            await _database.Disks.AddAsync(disk);
        }
        
        await _database.SaveChangesAsync();
        
        if (disk.DiskMetrics != null!)
        {
            foreach (DiskMetricsDBO diskMetrics in disk.DiskMetrics)
            {
                await _diskMetricsRepository.Add(diskMetrics);
            }
        }
    }
}