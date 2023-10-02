using Microsoft.AspNetCore.Mvc;
using SNMPPollingService.SNMP.Manager;
using SNMPPollingService.SNMP.Request;
using SNMPPollingService.SNMP.Result;

namespace SNMPPollingService.Controllers;

[Route("SNMP")]
public class SNMPController : Controller
{
    private readonly ISNMPManager _snmpManager;
    
    public SNMPController(ISNMPManager snmpManager)
    {
        _snmpManager = snmpManager;
    }

    [HttpPost("GetBulkWalk")]
    public async Task<IActionResult> GetBulkWalk([FromBody] SNMPConnectionInfo snmpConnectionInfo, string oid)
    {
        ISNMPResult result = await _snmpManager.BulkWalkAsync(snmpConnectionInfo, oid);

        return Ok(result.Variables.Select(variable => new
        {
            oid = variable.Id.ToString(),
            value = variable.Data.ToString()
        }));
    }
}