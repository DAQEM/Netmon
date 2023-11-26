using Netmon.Models.Component.Cpu;

namespace Netmon.SNMPPolling.DTO;

public class CpuDTO
{
    public int Index { get; set; }
    public List<CpuCoreDTO> Cores { get; set; }  = null!;
    
    public static CpuDTO FromCpu(ICpu arg)
    {
        return new CpuDTO
        {
            Index = arg.Index,
            Cores = arg.Cores.Select(CpuCoreDTO.FromCpuCore).ToList()
        };
    }
}