using DevicesLib.Entities.Component.Cpu.Core;

namespace DevicesLib.Entities.Component.Cpu;

public class Cpu : ICpu
{
    public int OneMinuteLoad { get; set; }
    public int FiveMinuteLoad { get; set; }
    public int FifteenMinuteLoad { get; set; }
    
    public List<ICpuCore> Cores { get; set; } = new();
}