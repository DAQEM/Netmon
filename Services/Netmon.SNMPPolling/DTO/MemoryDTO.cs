using Netmon.Models.Component.Memory;

namespace Netmon.SNMPPolling.DTO;

public class MemoryDTO
{
    public int Index { get; set; }
    public string Name { get; set; } = null!;
    
    public static MemoryDTO FromMemory(IMemory arg)
    {
        return new MemoryDTO
        {
            Index = arg.Index,
            Name = arg.Name
        };
    }
}