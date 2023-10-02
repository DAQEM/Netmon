namespace DevicesLib.Entities.Component.Cpu.Core;

public class CpuCore : ICpuCore
{
    public int Index { get; }
    public string Name { get; set; }
    public int Load { get; set; }
    
    public CpuCore(int index, string name, int load)
    {
        Index = index;
        Name = name;
        Load = load;
    }
}