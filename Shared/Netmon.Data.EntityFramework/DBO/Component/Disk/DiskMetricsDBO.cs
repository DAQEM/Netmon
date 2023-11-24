using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Netmon.Models.Component.Disk.Metric;

namespace Netmon.Data.EntityFramework.DBO.Component.Disk;

public class DiskMetricsDBO : IComponentMetricDBO
{
    [Key]
    public Guid Id { get; set; }
    
    [ForeignKey(nameof(Disk))]
    public Guid DiskId { get; set; }
    
    [Required]
    public DateTime Timestamp { get; set; }
    
    [Required]
    public int AllocationUnits { get; set; }
    
    [Required]
    public int TotalSpace { get; set; }
    
    [Required]
    public int UsedSpace { get; set; }
    
    public DiskDBO Disk { get; set; } = null!;

    public static DiskMetricsDBO FromDiskMetric(IDiskMetric diskMetric)
    {
        return new DiskMetricsDBO
        {
            Id = Guid.NewGuid(),
            Timestamp = diskMetric.Timestamp,
            AllocationUnits = diskMetric.AllocationUnits,
            TotalSpace = diskMetric.TotalSpace,
            UsedSpace = diskMetric.UsedSpace
        };
    }

    public IDiskMetric ToDiskMetric()
    {
        return new DiskMetric
        {
            Timestamp = Timestamp,
            AllocationUnits = AllocationUnits,
            TotalSpace = TotalSpace,
            UsedSpace = UsedSpace
        };
    }
}