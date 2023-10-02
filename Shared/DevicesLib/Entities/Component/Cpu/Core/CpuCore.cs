using DevicesLib.DBO.Component.Cpu.Core;

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

    public CpuCoreDBO ToDBO()
    {
        return new CpuCoreDBO
        {
            Index = Index,
            Name = Name,
            CpuCoreMetrics = new List<CpuCoreMetricsDBO>
            {
                new()
                {
                    Id = Guid.NewGuid(),
                    Timestamp = DateTime.Now,
                    Load = Load
                }
            }
        };
    }
}