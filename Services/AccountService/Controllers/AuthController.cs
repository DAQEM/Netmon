﻿using AccountService.Database;
using AccountService.Entities;
using AccountService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AccountService.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly AccountDatabase _database;
    
    public AuthController(AccountDatabase database)
    {
        _database = database;
    }
    
    [HttpPost]
    public IActionResult VerifySession(VerifyModel model)
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
        
        User? user = _database.Accounts
            .Include(u => u.Roles)
            .FirstOrDefault(u => u.Id == session.UserId);

        if (user == null)
        {
            Response.StatusCode = 400;
            return new JsonResult(new
            {
                Error = "Invalid session id"
            });
        }

        return new JsonResult(new UserDetailsModel
        {
            Username = user.Username,
            Email = user.Email,
            Roles = user.Roles.Select(r => r.Name).ToList(),
            Session = new SessionDetailsModel
            {
                Id = session.Token,
                Expires = session.ExpirationDate
            }
        });
    }
}