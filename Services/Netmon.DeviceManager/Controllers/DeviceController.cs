using Microsoft.AspNetCore.Mvc;
using Netmon.Data.DBO.Device;
using Netmon.Data.Repositories.Read.Device;
using Netmon.Data.Repositories.Write.Device;
using Netmon.Data.Write.Repositories.Device;

namespace Netmon.DeviceManager.Controllers;

[ApiController]
[Route("Device")]
public class DeviceController : BaseController
{
    private readonly IDeviceReadRepository _deviceReadRepository;
    
    public DeviceController(IDeviceReadRepository deviceReadRepository)
    {
        _deviceReadRepository = deviceReadRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _deviceReadRepository.GetAll());
    }
}