using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Netmon.Models.Component.Memory.Metric;

namespace Netmon.Data.EntityFramework.DBO.Component.Memory;

public class MemoryMetricsDBO : IComponentMetricDBO
{
    [Key]
    public Guid Id { get; set; }
    
    [ForeignKey(nameof(Memory))]
    public Guid MemoryId { get; set; }
    
    [Required]
    public DateTime Timestamp { get; set; }
    
    [Required]
    public int AllocationUnits { get; set; }
    
    [Required]
    public int TotalSpace { get; set; }
    
    [Required]
    public int UsedSpace { get; set; }
    
    public MemoryDBO Memory { get; set; } = null!;

    public static MemoryMetricsDBO FromMemoryMetric(IMemoryMetric arg)
    {
        return new MemoryMetricsDBO
        {
            Id = Guid.NewGuid(),
            Timestamp = arg.Timestamp,
            AllocationUnits = arg.AllocationUnits,
            TotalSpace = arg.TotalSpace,
            UsedSpace = arg.UsedSpace
        };
    }

    public IMemoryMetric ToMemoryMetric()
    {
        return new MemoryMetric
        {
            Timestamp = Timestamp,
            AllocationUnits = AllocationUnits,
            TotalSpace = TotalSpace,
            UsedSpace = UsedSpace
        };
    }
}