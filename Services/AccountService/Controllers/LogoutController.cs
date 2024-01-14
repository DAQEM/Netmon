using AccountService.Database;
using AccountService.Entities;
using AccountService.Models;
using Microsoft.AspNetCore.Mvc;

namespace AccountService.Controllers;

[ApiController]
[Route("[controller]")]
public class LogoutController(AccountDatabase database) : ControllerBase
{
    [HttpPost]
    public IActionResult Logout(LogoutModel model)
    {
        Session? session = database.Sessions.Find(model.SessionId);
        
        if (session == null)
        {
            Response.StatusCode = 400;
            return new JsonResult(new
            {
                Error = "Invalid session id"
            });
        }
        
        database.Sessions.Remove(session);
        database.SaveChanges();
        
        return Ok();
    }
}