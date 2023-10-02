using DevicesLib.DBO.Component.Cpu;

namespace DevicesLib.Repositories.Component.Cpu;

public interface ICpuRepository
{
    Task AddOrUpdateCpu(CpuDBO cpu);
}