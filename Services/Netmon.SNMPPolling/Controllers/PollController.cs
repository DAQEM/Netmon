using Microsoft.AspNetCore.Mvc;
using Netmon.Data.Services.Write.Device;
using Netmon.Models.Device;
using Netmon.SNMPPolling.DTO;
using Netmon.SNMPPolling.SNMP.Poll.Device;
using Netmon.SNMPPolling.SNMP.Request;

namespace Netmon.SNMPPolling.Controllers;

[ApiController]
[Route("Poll")]
public class PollController(IDeviceWriteService deviceWriteService, IDevicePoller devicePoller)
    : ControllerBase
{
    [HttpPost("Device")]
    public async Task<IActionResult> Device([FromBody] SNMPConnectionDTO connectionInfo)
    {
        SNMPConnectionInfo snmpConnectionInfo = connectionInfo.ToSNMPConnectionInfo();
        IDevice? device = await devicePoller.PollFull(snmpConnectionInfo);

        if (device is null) return NotFound();
        
        await deviceWriteService.AddOrUpdateFullDevice(device);
        
        return Ok(DeviceOverviewDTO.FromDevice(device));
    }
}