using DevicesLib.Entities.Component.Cpu.Core;

namespace DevicesLib.Entities.Component.Cpu;

public class Cpu : ICpu
{
    public float OneMinuteLoad { get; set; }
    public float FiveMinuteLoad { get; set; }
    public float FifteenMinuteLoad { get; set; }
    
    public List<ICpuCore> Cores { get; set; } = new();
}