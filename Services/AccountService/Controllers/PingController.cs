using Microsoft.AspNetCore.Mvc;

namespace AccountService.Controllers;

[ApiController]
[Route("[controller]")]
public class PingController
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