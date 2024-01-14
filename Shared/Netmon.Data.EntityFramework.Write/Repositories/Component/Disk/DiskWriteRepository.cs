using Microsoft.EntityFrameworkCore;
using Netmon.Data.DBO.Component.Disk;
using Netmon.Data.EntityFramework.Database;
using Netmon.Data.Repositories.Write.Component.Disk;

namespace Netmon.Data.Write.Repositories.Component.Disk;

public class DiskWriteRepository(DevicesDatabase database, IDiskMetricsWriteRepository diskMetricsWriteRepository)
    : IDiskWriteRepository
{
    public async Task AddOrUpdate(DiskDBO disk)
    {
        if (disk == null)
        {
            throw new ArgumentNullException(nameof(disk));
        }
        
        DiskDBO? existingDisk = await database.Disks.FirstOrDefaultAsync(d => d.DeviceId == disk.DeviceId && d.Index == disk.Index);

        if (existingDisk == null)
        {
            disk.Id = Guid.NewGuid();
            await database.Disks.AddAsync(disk);
        }
        else
        {
            disk.Id = existingDisk.Id;
            database.Entry(existingDisk).CurrentValues.SetValues(disk);
            
            if (disk.DiskMetrics != null!)
            {
                foreach (DiskMetricsDBO diskMetrics in disk.DiskMetrics)
                {
                    diskMetrics.DiskId = disk.Id;
                    await diskMetricsWriteRepository.Add(diskMetrics);
                }
            }
        }
    }
    
    public async Task SaveChanges()
    {
        await database.SaveChangesAsync();
    }
}