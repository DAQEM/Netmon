using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Netmon.AccountService.Model;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace Netmon.AccountService.Extensions;

public static class IdentityApiEndpointRouteBuilderExtensions
{
    private static readonly EmailAddressAttribute EmailAddressAttribute = new();
    
    public static IEndpointConventionBuilder MapMyIdentityApi(this IEndpointRouteBuilder endpoints)
    {
        ArgumentNullException.ThrowIfNull(endpoints);
        
        RouteGroupBuilder routeGroup = endpoints.MapGroup("");

        routeGroup.MapGet("/authenticate", async (HttpContext context, Database database) =>
        {
            if (context.User.Identity is not {IsAuthenticated: true})
            {
                return Results.Unauthorized();
            }
            
            int userId = int.Parse(context.User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");
            return await database.Users.Where(u => u.Id == userId).Select(u => new
            {
                u.Id,
                u.UserName,
                u.FullName,
                u.ProfileImageName,
                u.Email,
            }).FirstOrDefaultAsync() is { } user ? Results.Ok(user) : Results.Unauthorized();
            
        }).RequireAuthorization();

        routeGroup.MapGet("/users", async (Database database) => await database.Users.Select(user =>
            new {
                user.Id,
                user.UserName,
                user.FullName,
                user.ProfileImageName,
                user.Email,
            }).ToListAsync()).RequireAuthorization();
        
        routeGroup.MapGet("/users/{id}", async (Database database, int id) =>
        {
            User? dbUser = await database.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (dbUser is null)
            {
                return Results.NotFound();
            }
            
            return Results.Ok(new
            {
                dbUser.Id,
                dbUser.UserName,
                dbUser.FullName,
                dbUser.ProfileImageName,
                dbUser.Email,
            });
        }).RequireAuthorization();
        
        routeGroup.MapGet("/users/username/{userName}", async (Database database, string userName) =>
        {
            User? dbUser = await database.Users.FirstOrDefaultAsync(u => u.UserName == userName);
            if (dbUser is null)
            {
                return Results.NotFound();
            }
            
            return Results.Ok(new
            {
                dbUser.Id,
                dbUser.UserName,
                dbUser.FullName,
                dbUser.ProfileImageName,
                dbUser.Email,
            });
        }).RequireAuthorization();

        routeGroup.MapGet("/user", async (Database database, ClaimsPrincipal user) =>
        {
            User? dbUser = await database.Users.FirstOrDefaultAsync(u => u.Id == int.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0"));
            if (dbUser is null)
            {
                return Results.NotFound();
            }
            return Results.Ok(new
            {
                dbUser.Id,
                dbUser.UserName,
                dbUser.FullName,
                dbUser.ProfileImageName,
                dbUser.Email,
            });
        }).RequireAuthorization();

        routeGroup.MapDelete("/users/{id}", async (Database database, int id) =>
        {
            User? dbUser = await database.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (dbUser is null)
            {
                return Results.NotFound();
            }
            database.Users.Remove(dbUser);
            await database.SaveChangesAsync();
            return Results.Ok();
        }).RequireAuthorization();
        
        routeGroup.MapPost("/myregister", async Task<Results<Ok, ValidationProblem>>
            ([FromBody] MyRegisterRequest registration, [FromServices] IServiceProvider sp) =>
        {
            UserManager<User> userManager = sp.GetRequiredService<UserManager<User>>();

            if (!userManager.SupportsUserEmail)
            {
                throw new NotSupportedException(
                    $"{nameof(MapMyIdentityApi)} requires a user store with email support.");
            }

            IUserStore<User> userStore = sp.GetRequiredService<IUserStore<User>>();
            IUserEmailStore<User> emailStore = (IUserEmailStore<User>)userStore;
            string email = registration.Email;

            if (string.IsNullOrEmpty(email) || !EmailAddressAttribute.IsValid(email))
            {
                return CreateValidationProblem(IdentityResult.Failed(userManager.ErrorDescriber.InvalidEmail(email)));
            }

            string username = registration.UserName;

            if (string.IsNullOrEmpty(username) || username.Length < 3 || username.Length > 24)
            {
                return CreateValidationProblem(IdentityResult.Failed(GetUsernameErrors(username)));
            }

            User user = new()
            {
                UserName = registration.UserName,
                FullName = registration.FullName,
                ProfileImageName = registration.ProfileImageName
            };
            await userStore.SetUserNameAsync(user, registration.UserName, CancellationToken.None);
            await emailStore.SetEmailAsync(user, email, CancellationToken.None);
            IdentityResult result = await userManager.CreateAsync(user, registration.Password);

            if (!result.Succeeded)
            {
                return CreateValidationProblem(result);
            }

            return TypedResults.Ok();
        });
        
        return new IdentityEndpointsConventionBuilder(routeGroup);
    }

    private static IdentityError[] GetUsernameErrors(string username)
    {
        IdentityError[] errors = new IdentityError[1];
        
        if (string.IsNullOrEmpty(username))
        {
            errors[0] = new IdentityError
            {
                Code = "InvalidUserName",
                Description = "User name cannot be empty."
            };
        }
        else
            errors[0] = username.Length switch
            {
                < 3 => new IdentityError
                {
                    Code = "InvalidUserName", Description = "User name cannot be shorter than 3 characters."
                },
                > 24 => new IdentityError
                {
                    Code = "InvalidUserName", Description = "User name cannot be longer than 24 characters."
                },
                _ => errors[0]
            };

        return errors;
    }

    private static ValidationProblem CreateValidationProblem(IdentityResult result)
    {
        // We expect a single error code and description in the normal case.
        // This could be golfed with GroupBy and ToDictionary, but perf! :P
        Debug.Assert(!result.Succeeded);
        Dictionary<string, string[]> errorDictionary = new(1);

        foreach (IdentityError error in result.Errors)
        {
            string[] newDescriptions;

            if (errorDictionary.TryGetValue(error.Code, out string[]? descriptions))
            {
                newDescriptions = new string[descriptions.Length + 1];
                Array.Copy(descriptions, newDescriptions, descriptions.Length);
                newDescriptions[descriptions.Length] = error.Description;
            }
            else
            {
                newDescriptions = [error.Description];
            }

            errorDictionary[error.Code] = newDescriptions;
        }

        return TypedResults.ValidationProblem(errorDictionary);
    }

    private sealed class IdentityEndpointsConventionBuilder(IEndpointConventionBuilder inner) : IEndpointConventionBuilder
    {
        private IEndpointConventionBuilder InnerAsConventionBuilder => inner;

        public void Add(Action<EndpointBuilder> convention) => InnerAsConventionBuilder.Add(convention);
        public void Finally(Action<EndpointBuilder> finallyConvention) => InnerAsConventionBuilder.Finally(finallyConvention);
    }

    [AttributeUsage(AttributeTargets.Parameter)]
    private sealed class FromBodyAttribute : Attribute, IFromBodyMetadata;

    [AttributeUsage(AttributeTargets.Parameter)]
    private sealed class FromServicesAttribute : Attribute, IFromServiceMetadata;
}