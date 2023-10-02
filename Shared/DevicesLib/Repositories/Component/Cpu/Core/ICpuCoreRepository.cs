using DevicesLib.DBO.Component.Cpu.Core;

namespace DevicesLib.Repositories.Component.Cpu.Core;

public interface ICpuCoreRepository
{
    Task AddOrUpdateCpu(CpuCoreDBO cpuCore);
}