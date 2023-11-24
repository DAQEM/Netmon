using Microsoft.AspNetCore.Mvc;
using Netmon.SNMPPolling.DTO;
using Netmon.SNMPPolling.SNMP.Manager;
using Netmon.SNMPPolling.SNMP.Request;
using Netmon.SNMPPolling.SNMP.Result;

namespace Netmon.SNMPPolling.Controllers;

[Route("SNMP")]
public class SNMPController : Controller
{
    private readonly ISNMPManager _snmpManager;
    
    public SNMPController(ISNMPManager snmpManager)
    {
        _snmpManager = snmpManager;
    }

    [HttpPost("GetBulkWalk")]
    public async Task<IActionResult> GetBulkWalk([FromBody] SNMPConnectionDTO snmpConnectionDto, string oid, int timeoutMillis)
    {
        SNMPConnectionInfo snmpConnectionInfo = snmpConnectionDto.ToSNMPConnectionInfo();
        ISNMPResult result = await _snmpManager.BulkWalkAsync(snmpConnectionInfo, oid, timeoutMillis);

        return Ok(result.Variables.Select(variable => new
        {
            oid = variable.Id.ToString(),
            value = variable.Data.ToString()
        }));
    }
}