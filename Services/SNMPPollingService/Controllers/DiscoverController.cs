using Microsoft.AspNetCore.Mvc;
using SNMPPollingService.SNMP.Manager;
using SNMPPollingService.SNMP.MIB.System;
using SNMPPollingService.SNMP.Request;

namespace SNMPPollingService.Controllers;

[ApiController]
[Route("Discover")]
public class DiscoverController : Controller
{
    private readonly ISNMPManager _snmpManager;
    
    public DiscoverController(ISNMPManager snmpManager)
    {
        _snmpManager = snmpManager;
    }
    
    [HttpPost("Details")]
    public async Task<IActionResult> Details([FromBody] SNMPConnectionInfo connectionInfo)
    {
        SystemMIB systemMIB = await new SystemMIB().Poll(_snmpManager, connectionInfo);

        return Ok(new
        {
            SysDescr = systemMIB.SysDescr.ToString(),
            SysObjectID = systemMIB.SysObjectID.ToString(),
            SysUpTime = systemMIB.SysUpTime.ToTimeSpan(),
            SysContact = systemMIB.SysContact.ToString(),
            SysName = systemMIB.SysName.ToString(),
            SysLocation = systemMIB.SysLocation.ToString(),
            SysServices = systemMIB.SysServices.ToInt32() 
        });
    }
}