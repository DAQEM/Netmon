using Netmon.Models.Component.Cpu.Core;

namespace Netmon.SNMPPolling.DTO;

public class CpuCoreDTO
{
    public int Index { get; set; }
    public string Name { get; set; } = null!;
    
    public static CpuCoreDTO FromCpuCore(ICpuCore arg)
    {
        return new CpuCoreDTO
        {
            Index = arg.Index,
            Name = arg.Name
        };
    }
}