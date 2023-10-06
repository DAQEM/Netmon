using Microsoft.AspNetCore.Mvc;
using Netmon.Data.Repositories.Read.Device;

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