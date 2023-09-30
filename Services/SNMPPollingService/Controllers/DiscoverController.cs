using Microsoft.AspNetCore.Mvc;
using SNMPPollingService.Entities.Device;
using SNMPPollingService.SNMP.Manager;
using SNMPPollingService.SNMP.MIB.System;
using SNMPPollingService.SNMP.Poll.Device;
using SNMPPollingService.SNMP.Request;

namespace SNMPPollingService.Controllers;

[ApiController]
[Route("Discover")]
public class DiscoverController : Controller
{
    private readonly IDevicePoller _devicePoller;
    
    public DiscoverController(IDevicePoller devicePoller)
    {
        _devicePoller = devicePoller;
    }
    
    [HttpPost("Details")]
    public async Task<IActionResult> Details([FromBody] SNMPConnectionInfo connectionInfo)
    {
        IDevice device = await _devicePoller.PollDetails(connectionInfo);
        
        return Ok(new
        {
            device.IpAddress,
            device.Port,
            device.Community,
            device.Name,
            device.Location,
            device.Contact
        });
    }
}