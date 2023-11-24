using Netmon.Data.Services.Read.Component.Disk;
using Netmon.Models.Component.Disk.Metric;

namespace Netmon.Data.Services.Read.Services.Component.Disk;

public class DiskMetricsReadService : IDiskMetricReadService
{
    
    public async Task<List<IDiskMetric>> GetAll()
    {
        throw new NotImplementedException();
    }

    public async Task<IDiskMetric?> GetById(Guid id)
    {
        throw new NotImplementedException();
    }
    
    public async Task<List<IDiskMetric>> GetByComponentId(Guid componentId)
    {
        throw new NotImplementedException();
    }

    public async Task<List<IDiskMetric>> GetByComponentIds(List<Guid> componentIds)
    {
        throw new NotImplementedException();
    }
}