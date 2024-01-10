using Netmon.Data.Repositories.Read.Device;
using Netmon.Data.Services.Read.Device;
using Netmon.Models.Device.Connection;

namespace Netmon.Data.Services.Read.Services.Device;

public class DeviceConnectionReadService : IDeviceConnectionReadService
{
    private readonly IDeviceConnectionReadRepository _deviceConnectionReadRepository;
    
    public DeviceConnectionReadService(IDeviceConnectionReadRepository deviceConnectionReadRepository)
    {
        _deviceConnectionReadRepository = deviceConnectionReadRepository;
    }
    
    public async Task<List<IDeviceConnection>> GetAll()
    {
        return (await _deviceConnectionReadRepository.GetAll())
            .Select(x => x.ToDeviceConnection())
            .ToList();
    }

    public Task<IDeviceConnection?> GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<IDeviceConnection?> GetByDeviceId(Guid id)
    {
        return (await _deviceConnectionReadRepository.GetByDeviceId(id))?.ToDeviceConnection();
    }
}