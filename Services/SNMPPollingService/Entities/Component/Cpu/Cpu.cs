using SNMPPollingService.Entities.Component.Cpu.Core;

namespace SNMPPollingService.Entities.Component.Cpu;

public class Cpu : ICpu
{
    public float OneMinuteLoad { get; set; }
    public float FiveMinuteLoad { get; set; }
    public float FifteenMinuteLoad { get; set; }
    
    public List<ICpuCore> Cores { get; set; } = new();
    
    public Cpu(float oneMinuteLoad, float fiveMinuteLoad, float fifteenMinuteLoad, List<ICpuCore> cores)
    {
        OneMinuteLoad = oneMinuteLoad;
        FiveMinuteLoad = fiveMinuteLoad;
        FifteenMinuteLoad = fifteenMinuteLoad;
        Cores = cores;
    }
}