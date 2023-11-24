using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Netmon.Models.Component.Cpu.Metric;

namespace Netmon.Data.EntityFramework.DBO.Component.Cpu;

public class CpuMetricsDBO : IComponentMetricDBO
{
    [Key]
    public Guid Id { get; set; }
    
    [ForeignKey(nameof(Cpu))]
    public Guid CpuId { get; set; }
    
    [Required]
    public DateTime Timestamp { get; set; }
    
    [Required]
    public int OneMinuteLoad { get; set; }
    
    [Required]
    public int FiveMinuteLoad { get; set; }
    
    [Required]
    public int FifteenMinuteLoad { get; set; }
    
    public CpuDBO Cpu { get; set; } = null!;

    public static CpuMetricsDBO FromCpuMetric(ICpuMetric arg)
    {
        return new CpuMetricsDBO
        {
            Id = Guid.NewGuid(),
            Timestamp = arg.Timestamp,
            OneMinuteLoad = arg.OneMinuteLoad,
            FiveMinuteLoad = arg.FiveMinuteLoad,
            FifteenMinuteLoad = arg.FifteenMinuteLoad
        };
    }

    public ICpuMetric ToCpuMetric()
    {
        return new CpuMetric
        {
            Timestamp = Timestamp,
            OneMinuteLoad = OneMinuteLoad,
            FiveMinuteLoad = FiveMinuteLoad,
            FifteenMinuteLoad = FifteenMinuteLoad
        };
    }
}