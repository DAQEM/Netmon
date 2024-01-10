using Microsoft.EntityFrameworkCore;
using Netmon.Data.DBO.Component.Interface;
using Netmon.Data.EntityFramework.Database;
using Netmon.Data.Repositories.Read.Component.Interface;
using Netmon.Models.Component.Interface;

namespace Netmon.Data.EntityFramework.Read.Repositories.Component.Interface;

public class InterfaceReadRepository : IInterfaceReadRepository
{
    private readonly DevicesDatabase _database;
    
    private readonly IInterfaceMetricReadRepository _interfaceMetricsReadRepository;

    public InterfaceReadRepository(DevicesDatabase database, IInterfaceMetricReadRepository interfaceMetricsReadRepository)
    {
        _database = database;
        _interfaceMetricsReadRepository = interfaceMetricsReadRepository;
    }

    public async Task<List<InterfaceDBO>> GetAll()
    {
        return await _database.Interfaces.ToListAsync();
    }

    public async Task<InterfaceDBO?> GetById(Guid id)
    {
        return await _database.Interfaces.FirstOrDefaultAsync(device => device.Id == id);
    }
    
    public async Task<List<InterfaceDBO>> GetByDeviceId(Guid deviceId)
    {
        return await _database.Interfaces.Where(@interface => @interface.DeviceId == deviceId).ToListAsync();
    }

    public async Task<List<InterfaceDBO>> GetByDeviceIdWithMetrics(Guid deviceId)
    {
        return await _database.Interfaces
            .Include(@interface => @interface.InterfaceMetrics)
            .Where(@interface => @interface.DeviceId == deviceId)
            .Select(@interface => new InterfaceDBO
            {
                Index = @interface.Index,
                Name = @interface.Name,
                Type = @interface.Type,
                PhysAddress = @interface.PhysAddress,
                InterfaceMetrics = @interface.InterfaceMetrics.Select(metric => new InterfaceMetricsDBO
                {
                    Timestamp = metric.Timestamp,
                    InOctets = metric.InOctets,
                    OutOctets = metric.OutOctets
                }).ToList()
            })
            .ToListAsync();
    }
    
    public async Task<List<InterfaceDBO>> GetByDeviceIdWithMetrics(Guid deviceId, DateTime from, DateTime to)
    {
        return await _database.Interfaces
            .Include(@interface => @interface.InterfaceMetrics)
            .Where(@interface => @interface.DeviceId == deviceId)
            .Select(@interface => new InterfaceDBO
            {
                Index = @interface.Index,
                Name = @interface.Name,
                Type = @interface.Type,
                PhysAddress = @interface.PhysAddress,
                InterfaceMetrics = @interface.InterfaceMetrics
                    .Where(metric => metric.Timestamp >= from && metric.Timestamp <= to)
                    .Select(metric => new InterfaceMetricsDBO
                {
                    Timestamp = metric.Timestamp,
                    InOctets = metric.InOctets,
                    OutOctets = metric.OutOctets
                })
                    .OrderBy(metric => metric.Timestamp)
                    .ToList()
            })
            .ToListAsync();
    }
}