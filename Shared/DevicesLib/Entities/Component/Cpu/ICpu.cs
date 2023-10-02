using DevicesLib.Entities.Component.Cpu.Core;

namespace DevicesLib.Entities.Component.Cpu;

public interface ICpu : IComponent
{
    public int OneMinuteLoad { get; set; }
    public int FiveMinuteLoad { get; set; }
    public int FifteenMinuteLoad { get; set; }
    
    public List<ICpuCore> Cores { get; set; }
}