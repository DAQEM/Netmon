namespace SNMPPollingService.Entities.Component.Cpu.Core;

public class CpuCore : ICpuCore
{
    public string Name { get; set; }
    public float Load { get; set; }
    
    public CpuCore(string name, float load)
    {
        Name = name;
        Load = load;
    }
}