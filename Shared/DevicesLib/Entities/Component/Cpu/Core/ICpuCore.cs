namespace DevicesLib.Entities.Component.Cpu.Core;

public interface ICpuCore
{
    public int Index { get; }
    public string Name { get; set; }
    public int Load { get; set; }
}