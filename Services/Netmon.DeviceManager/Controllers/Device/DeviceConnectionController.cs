using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Netmon.Data.Services.Read.Device;
using Netmon.Data.Services.Write.Device;

namespace Netmon.DeviceManager.Controllers.Device;

[ApiController]
[Authorize]
[Route("Device/Connection")]
public class DeviceConnectionController(
    IDeviceConnectionReadService deviceConnectionReadService,
    IDeviceConnectionWriteService deviceConnectionWriteService)
    : BaseController
{
    private readonly IDeviceConnectionWriteService _deviceConnectionWriteService = deviceConnectionWriteService;

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await deviceConnectionReadService.GetAll());
    }
}