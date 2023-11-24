using Microsoft.AspNetCore.Mvc;
using Netmon.Data.EntityFramework.DBO.Device;
using Netmon.Data.Repositories.Write.Device;
using Netmon.Data.Services.Write.Device;
using Netmon.Models.Device;
using Netmon.SNMPPolling.DTO;
using Netmon.SNMPPolling.SNMP.Poll.Device;
using Netmon.SNMPPolling.SNMP.Request;

namespace Netmon.SNMPPolling.Controllers;

[ApiController]
[Route("Poll")]
public class PollController : ControllerBase
{
    private readonly IDeviceWriteService _deviceWriteService;
    private readonly IDevicePoller _devicePoller;
    
    public PollController(IDeviceWriteService deviceWriteService, IDevicePoller devicePoller)
    {
        _deviceWriteService = deviceWriteService;
        _devicePoller = devicePoller;
    }

    [HttpPost("Device")]
    public async Task<IActionResult> Device([FromBody] SNMPConnectionDTO connectionInfo)
    {
        SNMPConnectionInfo snmpConnectionInfo = connectionInfo.ToSNMPConnectionInfo();
        IDevice? device = await _devicePoller.PollFull(snmpConnectionInfo);

        if (device == null) return NotFound();
        
        await _deviceWriteService.AddOrUpdateFullDevice(device);
        
        return Ok(DeviceOverviewDTO.FromDevice(device));
    }
}