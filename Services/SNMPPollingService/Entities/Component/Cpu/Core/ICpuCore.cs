namespace SNMPPollingService.Entities.Component.Cpu.Core;

public interface ICpuCore
{
    public string Name { get; set; }
    public float Load { get; set; }
}