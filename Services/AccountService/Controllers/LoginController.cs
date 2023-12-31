﻿using AccountService.Database;
using AccountService.Entities;
using AccountService.Models;
using Microsoft.AspNetCore.Mvc;

namespace AccountService.Controllers;

[ApiController]
[Route("[controller]")]
public class LoginController : ControllerBase
{
    private readonly AccountDatabase _database;
    
    public LoginController(AccountDatabase database)
    {
        _database = database;
    }
    
    [HttpPost]
    public IActionResult Login(LoginModel model)
    {
        //check if email exists
        User? user = _database.Accounts
            .Where(u => u.Email == model.Email)
            .Select(u => new User
            {
                Id = u.Id,
                Password = u.Password,
            })
            .FirstOrDefault();
        
         if (user == null)
        {
            Response.StatusCode = 400;
            return new JsonResult(new
            {
                Error = "Invalid email or password"
            });
        }
         
        //check if password is correct
        if (!BCrypt.Net.BCrypt.Verify(model.Password, user.Password))
        {
            Response.StatusCode = 400;
            return new JsonResult(new
            {
                Error = "Invalid email or password"
            });
        }

        //create session
        Session session = new(user.Id);
        
        //add session to database
        _database.Sessions.Add(session);
        _database.SaveChanges();

        return new JsonResult(new SessionDetailsModel
        {
            Id = session.Token,
            Expires = session.ExpirationDate
        });
    }
}