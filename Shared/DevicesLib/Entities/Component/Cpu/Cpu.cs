using DevicesLib.DBO.Component.Cpu;
using DevicesLib.Entities.Component.Cpu.Core;

namespace DevicesLib.Entities.Component.Cpu;

public class Cpu : ICpu
{
    public int Index { get; set; }
    public int OneMinuteLoad { get; set; }
    public int FiveMinuteLoad { get; set; }
    public int FifteenMinuteLoad { get; set; }
    
    public List<ICpuCore> Cores { get; set; } = new();
    
    public CpuDBO ToDBO()
    {
        return new CpuDBO
        {
            Index = Index,
            CpuMetrics = new List<CpuMetricsDBO>
            {
                new()
                {
                    Id = Guid.NewGuid(),
                    Timestamp = DateTime.Now,
                    OneMinuteLoad = OneMinuteLoad,
                    FiveMinuteLoad = FiveMinuteLoad,
                    FifteenMinuteLoad = FifteenMinuteLoad
                }
            },
            CpuCores = Cores.Select(core => core.ToDBO()).ToList()
        };
    }
}