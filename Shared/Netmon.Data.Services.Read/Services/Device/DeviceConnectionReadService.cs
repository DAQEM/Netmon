using Netmon.Data.DBO.Device;
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
    
    public async Task<List<DeviceConnection>> GetAll()
    {
        throw new NotImplementedException();
    }

    public async Task<DeviceConnection?> GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<DeviceConnection?> GetByDeviceId(Guid id)
    {
        DeviceConnectionDBO? deviceConnectionDBO = await _deviceConnectionReadRepository.GetByDeviceId(id);
        return deviceConnectionDBO?.ToDeviceConnection();
    }
}