using Microsoft.AspNetCore.Mvc;
using Netmon.Data.Repositories.Read.Device;
using Netmon.Data.Repositories.Write.Device;
using Netmon.DeviceManager.DTO.Device;
using Netmon.Models.Device;

namespace Netmon.DeviceManager.Controllers;

[ApiController]
[Route("Device")]
public class DeviceController : BaseController
{
    private readonly IDeviceReadRepository _deviceReadRepository;
    private readonly IDeviceWriteRepository _deviceWriteRepository;
    
    public DeviceController(IDeviceReadRepository deviceReadRepository, IDeviceWriteRepository deviceWriteRepository)
    {
        _deviceReadRepository = deviceReadRepository;
        _deviceWriteRepository = deviceWriteRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _deviceReadRepository.GetAll());
    }
    
    [HttpPost]
    public async Task<IActionResult> Post(DeviceCreateDTO deviceCreateDTO)
    {
        IDevice device = await _deviceWriteRepository.AddDeviceWithConnection(deviceCreateDTO.ToDevice());
        await _deviceWriteRepository.SaveChanges();
        return Ok(device);
    }
}