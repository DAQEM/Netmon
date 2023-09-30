using SNMPPollingService.Entities.Component.Cpu.Core;

namespace SNMPPollingService.Entities.Component.Cpu;

public interface ICpu : IComponent
{
    public float OneMinuteLoad { get; set; }
    public float FiveMinuteLoad { get; set; }
    public float FifteenMinuteLoad { get; set; }
    
    public List<ICpuCore> Cores { get; set; }
}