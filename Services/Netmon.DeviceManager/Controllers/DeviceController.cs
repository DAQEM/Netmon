using Microsoft.AspNetCore.Mvc;
using Netmon.Data.DBO.Device;
using Netmon.Data.Repositories.Write.Device;
using Netmon.Data.Write.Repositories.Device;

namespace Netmon.DeviceManager.Controllers;

[ApiController]
[Route("Device")]
public class DeviceController : BaseController
{
    private readonly IDeviceWriteRepository _deviceWriteRepository;
    
    public DeviceController(IDeviceWriteRepository deviceWriteRepository)
    {
        _deviceWriteRepository = deviceWriteRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok();
    }
}