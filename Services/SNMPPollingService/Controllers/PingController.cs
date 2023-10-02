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
}