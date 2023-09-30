using System.Net;
using Lextm.SharpSnmpLib;
using Lextm.SharpSnmpLib.Messaging;
using Microsoft.AspNetCore.Mvc;

namespace SNMPPollingService.Controllers;

[ApiController]
[Route("Ping")]
public class PingController : ControllerBase
{
    [HttpGet]
    public IActionResult Ping()
    {
        return new JsonResult(new
        {
            message = "Pong"
        });
    }
    
    [HttpGet("Test")]
    public async Task<IActionResult> Test(string oid, string community)
    {
        List<Variable> result = new();

        IPEndPoint ipEndPoint = new(IPAddress.Parse("158.62.204.5"), 161);
        
        await Messenger.BulkWalkAsync(VersionCode.V2,
            ipEndPoint,
            new OctetString(community),
            OctetString.Empty,
            new ObjectIdentifier(oid),
            result,
            10,
            WalkMode.WithinSubtree,
            null!,
            null!);
        
        return new JsonResult(result
            .GroupBy(variable => variable.Id.ToString().Split(".").Last())
            .Select(grouping => grouping
                .Select(variable => $"{variable.Id} {variable.Data.GetType().ToString().Split(".").Last()} {variable.Data.ToString()}")));
    }
}