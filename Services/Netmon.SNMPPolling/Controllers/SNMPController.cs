using Microsoft.AspNetCore.Mvc;
using Netmon.SNMPPolling.DTO;
using Netmon.SNMPPolling.SNMP.Manager;
using Netmon.SNMPPolling.SNMP.Request;
using Netmon.SNMPPolling.SNMP.Result;

namespace Netmon.SNMPPolling.Controllers;

[Route("SNMP")]
public class SNMPController(ISNMPManager snmpManager) : Controller
{
    [HttpPost("GetBulkWalk")]
    public async Task<IActionResult> GetBulkWalk([FromBody] SNMPConnectionDTO snmpConnectionDto, string oid, int timeoutMillis)
    {
        SNMPConnectionInfo snmpConnectionInfo = snmpConnectionDto.ToSNMPConnectionInfo();
        ISNMPResult result = await snmpManager.BulkWalkAsync(snmpConnectionInfo, oid, timeoutMillis);

        if (!result.Variables.Any()) return NotFound();
        
        return Ok(result.Variables.Select(variable => new
        {
            oid = variable.Id.ToString(),
            value = variable.Data.ToString()
        }));
    }
}