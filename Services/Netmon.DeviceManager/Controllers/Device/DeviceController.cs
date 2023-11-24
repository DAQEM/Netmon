using Microsoft.AspNetCore.Mvc;
using Netmon.Data.Services.Read.Device;
using Netmon.Data.Services.Write.Device;
using Netmon.DeviceManager.DTO.Device;
using Netmon.Models.Device;

namespace Netmon.DeviceManager.Controllers.Device;

[ApiController]
[Route("Device")]
public class DeviceController : BaseController
{
    private readonly IDeviceReadService _deviceReadService;
    private readonly IDeviceWriteService _deviceWriteService;

    public DeviceController(IDeviceReadService deviceReadService, IDeviceWriteService deviceWriteService)
    {
        _deviceReadService = deviceReadService;
        _deviceWriteService = deviceWriteService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _deviceReadService.GetAll());
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetDeviceById(Guid id, bool includeConnection = false)
    {
        IDevice? device = await _deviceReadService.GetById(id, includeConnection);
        return device == null ? NoContent() : Ok(DeviceWithConnectionDTO.FromDeviceWithConnection(device));
    }
    
    [HttpPost]
    public async Task<IActionResult> Post(DeviceCreateDTO deviceCreateDTO)
    {
        IDevice device = deviceCreateDTO.ToDevice();
        device = await _deviceWriteService.AddDeviceWithConnection(device);
        DeviceWithConnectionDTO deviceDTO = DeviceWithConnectionDTO.FromDeviceWithConnection(device);
        return CreatedAtAction(nameof(GetDeviceById), new { id = deviceDTO.Id }, deviceDTO);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(Guid id, DeviceUpdateDTO deviceUpdateDTO)
    {
        Models.Device.Device device = deviceUpdateDTO.ToDevice();
        device.Id = id;
        await _deviceWriteService.UpdateWithConnection(device);
        return Ok(DeviceWithConnectionDTO.FromDeviceWithConnection(device));
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _deviceWriteService.Delete(id);
        return Ok();
    }
}