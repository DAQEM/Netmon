using AccountService.Database;
using AccountService.Entities;
using AccountService.Models;
using Microsoft.AspNetCore.Mvc;

namespace AccountService.Controllers;

[ApiController]
[Route("[controller]")]
public class RegisterController(AccountDatabase database) : ControllerBase
{
    [HttpPost]
    public IActionResult Register(RegisterModel model)
    {
        if (CheckIfEmailIsInUse(model, out string error)) return new JsonResult(new
        {
            Error = error
        });
        if (CheckIfUsernameIsInUse(model, out string error1)) return new JsonResult(new
        {
            Error = error1
        });
        if (CheckIfPasswordIsValid(model, out string error2)) return new JsonResult(new
        {
            Error = error2
        });

        //create encrypted password
        string salt = BCrypt.Net.BCrypt.GenerateSalt(12);
        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(model.Password, salt);
        
        //create user
        User user = new()
        {
            Username = model.Username,
            Email = model.Email,
            Password = hashedPassword,
            Salt = salt,
            Roles = new List<Role>
            {
                new()
                {
                    Name = "user"
                }
            }
        };
        
        //add user to database
        database.Accounts.Add(user);
        database.SaveChanges();
        
        //create session
        Session session = new(user.Id);

        //add session to database
        database.Sessions.Add(session);
        database.SaveChanges();
        
        //return session
        return Ok(new SessionDetailsModel
        {
            Id = session.Token,
            Expires = session.ExpirationDate
        });
    }

    private bool CheckIfEmailIsInUse(RegisterModel model, out string error)
    {
        if (database.Accounts.Any(u => u.Email == model.Email))
        {
            {
                error = "Email is already in use";
                return true;
            }
        }

        error = null!;
        return false;
    }

    private bool CheckIfUsernameIsInUse(RegisterModel model, out string error)
    {
        if (database.Accounts.Any(u => u.Username == model.Username))
        {
            {
                error = "Username is already in use";
                return true;
            }
        }

        error = null!;
        return false;
    }

    private bool CheckIfPasswordIsValid(RegisterModel model, out string error)
    {
        if (model.Password.Length < 8)
        {
            {
                error = "Password must be at least 8 characters long";
                return true;
            }
        }

        if (!model.Password.Any(char.IsUpper))
        {
            {
                error = "Password must contain at least one uppercase letter";
                return true;
            }
        }

        if (!model.Password.Any(char.IsLower))
        {
            {
                error = "Password must contain at least one lowercase letter";
                return true;
            }
        }

        if (!model.Password.Any(char.IsDigit))
        {
            {
                error = "Password must contain at least one digit";
                return true;
            }
        }

        error = null!;
        return false;
    }
}