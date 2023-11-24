using Microsoft.AspNetCore.Mvc;
using Netmon.Data.EntityFramework.DBO.Device;
using Netmon.Data.Repositories.Write.Device;
using Netmon.Models.Device;
using Netmon.SNMPPolling.SNMP.Poll.Device;
using Netmon.SNMPPolling.SNMP.Request;

namespace Netmon.SNMPPolling.Controllers;

[ApiController]
[Route("Poll")]
public class PollController : ControllerBase
{
    private readonly IDeviceWriteRepository _deviceWriteRepository;
    private readonly IDevicePoller _devicePoller;
    
    public PollController(IDeviceWriteRepository deviceWriteRepository, IDevicePoller devicePoller)
    {
        _deviceWriteRepository = deviceWriteRepository;
        _devicePoller = devicePoller;
    }

    [HttpPost("Device")]
    public async Task<IActionResult> Device([FromBody] SNMPConnectionInfo connectionInfo)
    {
        IDevice? device = await _devicePoller.PollFull(connectionInfo);

        if (device == null) return NotFound();
        
        await _deviceWriteRepository.AddOrUpdateFullDevice(device);
        await _deviceWriteRepository.SaveChanges();
        
        return Ok(device);
    }
}