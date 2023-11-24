using Microsoft.AspNetCore.Mvc;
using Netmon.Data.Repositories.Read.Device;
using Netmon.Data.Repositories.Write.Device;
using Netmon.Data.Services.Read.Device;
using Netmon.Data.Services.Write.Device;

namespace Netmon.DeviceManager.Controllers.Device;

[ApiController]
[Route("Device/Connection")]
public class DeviceConnectionController : BaseController
{
    private readonly IDeviceConnectionReadService _deviceConnectionReadService;
    private readonly IDeviceConnectionWriteService _deviceConnectionWriteService;
    
    public DeviceConnectionController(IDeviceConnectionReadService deviceConnectionReadService, IDeviceConnectionWriteService deviceConnectionWriteService)
    {
        _deviceConnectionReadService = deviceConnectionReadService;
        _deviceConnectionWriteService = deviceConnectionWriteService;
    }
    
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _deviceConnectionReadService.GetAll());
    }
}