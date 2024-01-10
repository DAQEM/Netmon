using Netmon.Data.DBO.Device;
using Netmon.Data.Repositories.Write.Component;
using Netmon.Models.Device.Connection;

namespace Netmon.Data.Repositories.Write.Device;

public interface IDeviceConnectionWriteRepository : IComponentWriteRepository<DeviceConnectionDBO>
{
}