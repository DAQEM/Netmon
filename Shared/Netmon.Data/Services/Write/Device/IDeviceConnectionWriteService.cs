using Netmon.Data.DBO.Device;
using Netmon.Data.Repositories.Write.Component;
using Netmon.Data.Services.Write.Component;
using Netmon.Models.Device.Connection;

namespace Netmon.Data.Services.Write.Device;

public interface IDeviceConnectionWriteService : IComponentWriteService<DeviceConnection>
{
}