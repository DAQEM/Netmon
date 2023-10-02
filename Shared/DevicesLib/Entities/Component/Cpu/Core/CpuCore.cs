namespace DevicesLib.Entities.Component.Cpu.Core;

public class CpuCore : ICpuCore
{
    public string Name { get; set; }
    public int Load { get; set; }
    
    public CpuCore(string name, int load)
    {
        Name = name;
        Load = load;
    }
}