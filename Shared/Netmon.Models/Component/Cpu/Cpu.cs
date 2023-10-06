using Netmon.Models.Component.Cpu.Core;
using Netmon.Models.Component.Cpu.Metric;

namespace Netmon.Models.Component.Cpu;

public class Cpu : ICpu
{
    public int Index { get; set; }
    public List<ICpuCore> Cores { get; set; }
    public List<ICpuMetric> Metrics { get; set; }
}