using Netmon.Data.DBO.Component.Disk;
using Netmon.Data.Services.Read.Component.Disk;
using Netmon.Models.Component.Disk.Metric;

namespace Netmon.Data.Services.Read.Services.Component.Disk;

public class DiskMetricsReadService : IDiskMetricReadService
{
    
    public async Task<List<DiskMetric>> GetAll()
    {
        throw new NotImplementedException();
    }

    public async Task<DiskMetric?> GetById(Guid id)
    {
        throw new NotImplementedException();
    }
    
    public async Task<List<DiskMetric>> GetByComponentId(Guid componentId)
    {
        throw new NotImplementedException();
    }

    public async Task<List<DiskMetric>> GetByComponentIds(List<Guid> componentIds)
    {
        throw new NotImplementedException();
    }
}