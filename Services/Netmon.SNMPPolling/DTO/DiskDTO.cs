using Netmon.Models.Component.Disk;

namespace Netmon.SNMPPolling.DTO;

public class DiskDTO
{
    public int Index { get; set; }
    public string MountingPoint { get; set; } = null!;
    
    public static DiskDTO FromDisk(IDisk arg)
    {
        return new DiskDTO
        {
            Index = arg.Index,
            MountingPoint = arg.MountingPoint
        };
    }
}