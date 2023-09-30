using Microsoft.AspNetCore.Mvc;
using SNMPPollingService.Entities.Device;
using SNMPPollingService.SNMP.Converter;
using SNMPPollingService.SNMP.Manager;
using SNMPPollingService.SNMP.MIB.HostResources;
using SNMPPollingService.SNMP.MIB.If;
using SNMPPollingService.SNMP.MIB.System;
using SNMPPollingService.SNMP.MIB.UCDavis;
using SNMPPollingService.SNMP.Request;

namespace SNMPPollingService.Controllers;

[Route("SNMP")]
public class SNMPController : Controller
{
    private readonly ISNMPManager _snmpManager;
    
    public SNMPController(ISNMPManager snmpManager)
    {
        _snmpManager = snmpManager;
    }

    [HttpPost("Test")]
    public async Task<IActionResult> Test([FromBody] SNMPConnectionInfo snmpConnectionInfo)
    {
        SystemMIB systemMIB = await new SystemMIB().Poll(_snmpManager, snmpConnectionInfo);
        HostResourcesMIB hostResourcesMIB = await new HostResourcesMIB().Poll(_snmpManager, snmpConnectionInfo);

        UCDavisMIB ucDavisMIB = await new UCDavisMIB().Poll(_snmpManager, snmpConnectionInfo);
        
        IfMIB ifMIB = await new IfMIB().Poll(_snmpManager, snmpConnectionInfo);
        
        IDevice device = MIBToDeviceConverter.ConvertToDevice(snmpConnectionInfo, systemMIB, hostResourcesMIB, ucDavisMIB, ifMIB);

        return Ok(device);
    }
}