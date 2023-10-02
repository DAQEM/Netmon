using DevicesLib.DBO.Component.Disk;

namespace DevicesLib.Repositories.Component.Disk;

public interface IDiskRepository
{
    Task AddOrUpdateDisk(DiskDBO disk);
}