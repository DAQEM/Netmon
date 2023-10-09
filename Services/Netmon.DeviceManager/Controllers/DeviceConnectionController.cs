using Microsoft.AspNetCore.Mvc;
using Netmon.Data.Repositories.Read.Device;
using Netmon.Data.Repositories.Write.Device;

namespace Netmon.DeviceManager.Controllers;

[ApiController]
[Route("Device/Connection")]
public class DeviceConnectionController : BaseController
{
    private readonly IDeviceConnectionReadRepository _deviceConnectionReadRepository;
    private readonly IDeviceConnectionWriteRepository _deviceConnectionWriteRepository;
    
    public DeviceConnectionController(IDeviceConnectionReadRepository deviceConnectionReadRepository, IDeviceConnectionWriteRepository deviceConnectionWriteRepository)
    {
        _deviceConnectionReadRepository = deviceConnectionReadRepository;
        _deviceConnectionWriteRepository = deviceConnectionWriteRepository;
    }
    
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _deviceConnectionReadRepository.GetAll());
    }
}