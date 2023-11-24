using Microsoft.EntityFrameworkCore;
using Netmon.Data.EntityFramework.Database;
using Netmon.Data.EntityFramework.DBO.Component.Interface;
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

    public async Task<List<IInterface>> GetAll()
    {
        return await _database.Interfaces.Select(dbo => dbo.ToInterface()).ToListAsync();
    }

    public async Task<IInterface?> GetById(Guid id)
    {
        return (await _database.Interfaces.FirstOrDefaultAsync(device => device.Id == id))?.ToInterface();
    }
    
    public async Task<List<IInterface>> GetByDeviceId(Guid deviceId)
    {
        return await _database.Interfaces.Where(@interface => @interface.DeviceId == deviceId).Select(dbo => dbo.ToInterface()).ToListAsync();
    }

    public async Task<List<IInterface>> GetByDeviceIdWithMetrics(Guid deviceId)
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
            .Select(dbo => dbo.ToInterface())
            .ToListAsync();
    }
    
    public async Task<List<IInterface>> GetByDeviceIdWithMetrics(Guid deviceId, DateTime from, DateTime to)
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
            .Select(dbo => dbo.ToInterface())
            .ToListAsync();
    }
}