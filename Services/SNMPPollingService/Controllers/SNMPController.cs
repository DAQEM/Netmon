using Microsoft.AspNetCore.Mvc;
using SNMPPollingService.Entities.Device;
using SNMPPollingService.SNMP.Poll.Device;
using SNMPPollingService.SNMP.Request;

namespace SNMPPollingService.Controllers;

[Route("SNMP")]
public class SNMPController : Controller
{
    private readonly IDevicePoller _devicePoller;
    
    public SNMPController(IDevicePoller devicePoller)
    {
        _devicePoller = devicePoller;
    }

    [HttpPost("Test")]
    public async Task<IActionResult> Test([FromBody] SNMPConnectionInfo snmpConnectionInfo)
    {
        IDevice device = await _devicePoller.PollFull(snmpConnectionInfo);

        return Ok(device);
    }
}