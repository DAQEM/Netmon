using Microsoft.AspNetCore.Mvc;
using Netmon.Data.DBO.Device;
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
    private readonly IDeviceConnectionReadRepository _deviceConnectionReadRepository;

    public DeviceController(IDeviceReadRepository deviceReadRepository, IDeviceWriteRepository deviceWriteRepository, 
        IDeviceConnectionReadRepository deviceConnectionReadRepository)
    {
        _deviceReadRepository = deviceReadRepository;
        _deviceWriteRepository = deviceWriteRepository;
        _deviceConnectionReadRepository = deviceConnectionReadRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok((await _deviceReadRepository.GetAll())
            .Select(DeviceDTO.FromDeviceDBO));
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetDeviceById(Guid id, bool includeConnection = false)
    {
        DeviceDBO? deviceDBO = await _deviceReadRepository.GetById(id);
        if (deviceDBO == null) return NoContent();
        DeviceWithConnectionDTO device = DeviceWithConnectionDTO.FromDeviceDBO(deviceDBO);
        
        if (includeConnection)
        {
            DeviceConnectionDBO? deviceConnectionDBO = await _deviceConnectionReadRepository.GetByDeviceId(id);
            if (deviceConnectionDBO != null)
            {
                device.Connection = DeviceConnectionDTO.FromDeviceConnectionDBO(deviceConnectionDBO);
            }
        }

        return Ok(device);
    }
    
    [HttpPost]
    public async Task<IActionResult> Post(DeviceCreateDTO deviceCreateDTO)
    {
        DeviceDBO deviceDBO = deviceCreateDTO.ToDeviceDBO();
        DeviceDBO? existingDevice = await _deviceReadRepository.GetByIpAddress(deviceDBO.IpAddress);
        if (existingDevice != null) return Conflict(new { message = "Device with this IP address already exists" });
        deviceDBO = await _deviceWriteRepository.AddDeviceWithConnection(deviceDBO);
        await _deviceWriteRepository.SaveChanges();
        DeviceWithConnectionDTO deviceDTO = DeviceWithConnectionDTO.FromDeviceDBOWithConnection(deviceDBO);
        return CreatedAtAction(nameof(GetDeviceById), new { id = deviceDBO.Id }, deviceDTO);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(Guid id, DeviceUpdateDTO deviceUpdateDTO)
    {
        DeviceDBO? deviceDBO = await _deviceReadRepository.GetById(id);
        if (deviceDBO == null) return NoContent();
        deviceDBO = deviceUpdateDTO.ToDeviceDBO(deviceDBO);
        await _deviceWriteRepository.UpdateWithConnection(deviceDBO);
        await _deviceWriteRepository.SaveChanges();
        return Ok(DeviceWithConnectionDTO.FromDeviceDBOWithConnection(deviceDBO));
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        DeviceDBO? deviceDBO = await _deviceReadRepository.GetById(id);
        if (deviceDBO == null) return NoContent();
        await _deviceWriteRepository.Delete(deviceDBO);
        await _deviceWriteRepository.SaveChanges();
        return Ok();
    }
}