using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Netmon.Data.DBO.Device;
using Netmon.Models.Component.Memory;

namespace Netmon.Data.DBO.Component.Memory;

public class MemoryDBO : IComponentDBO
{
    [Key]
    public Guid Id { get; set; }
    
    [ForeignKey(nameof(Device))]
    public Guid DeviceId { get; set; }
    
    [Required]
    public int Index { get; set; }
    
    [Required]
    public string Name { get; set; } = null!;
    
    public List<MemoryMetricsDBO> MemoryMetrics { get; set; } = null!;
    
    public DeviceDBO Device { get; set; } = null!;

    public static MemoryDBO FromMemory(IMemory arg)
    {
        return new MemoryDBO
        {
            Id = Guid.NewGuid(),
            Index = arg.Index,
            Name = arg.Name,
            MemoryMetrics = arg.Metrics.Select(MemoryMetricsDBO.FromMemoryMetric).ToList()
        };
    }
}