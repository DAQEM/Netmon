using Netmon.Data.DBO.Device;
using Netmon.Data.Repositories.Read.Component.Cpu;
using Netmon.Data.Repositories.Read.Component.Disk;
using Netmon.Data.Repositories.Read.Component.Interface;
using Netmon.Data.Repositories.Read.Component.Memory;
using Netmon.Data.Repositories.Read.Device;
using Netmon.Data.Services.Read.Device;

namespace Netmon.Data.Services.Read.Services.Device;

public class DeviceReadService : IDeviceReadService
{

    public async Task<List<Models.Device.Device>> GetAll()
    {
        throw new NotImplementedException();
    }

    public async Task<Models.Device.Device?> GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<Models.Device.Device?> GetByIpAddress(string deviceDBOIpAddress)
    {
        throw new NotImplementedException();
    }
}