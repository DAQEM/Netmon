using AccountService.Database;
using AccountService.Entities;
using AccountService.Models;
using Microsoft.AspNetCore.Mvc;

namespace AccountService.Controllers;

[ApiController]
[Route("[controller]")]
public class LogoutController : ControllerBase
{
    private readonly AccountDatabase _database;
    
    public LogoutController(AccountDatabase database)
    {
        _database = database;
    }
    
    [HttpPost]
    public IActionResult Logout(LogoutModel model)
    {
        Session? session = _database.Sessions.Find(model.SessionId);
        
        if (session == null)
        {
            Response.StatusCode = 400;
            return new JsonResult(new
            {
                Error = "Invalid session id"
            });
        }
        
        _database.Sessions.Remove(session);
        _database.SaveChanges();
        
        return Ok();
    }
}