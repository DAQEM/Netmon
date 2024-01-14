using System.Security.Claims;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Netmon.AccountService;
using Swashbuckle.AspNetCore.Filters;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        corsPolicyBuilder => corsPolicyBuilder.WithOrigins(builder.Configuration["Cors:AllowedOrigins"]?.Split(",") ?? Array.Empty<string>())
            .AllowAnyMethod()
            .AllowAnyHeader());
});

builder.Services.AddDataProtection()
    .PersistKeysToDbContext<Database>();

builder.Services.AddAuthentication(IdentityConstants.BearerScheme).AddBearerToken(IdentityConstants.BearerScheme);
builder.Services.AddAuthorizationBuilder();

string connectionString = builder.Configuration["MySQL:ConnectionString"] ?? throw new InvalidOperationException("MySQL connection string is not configured.");

builder.Services.AddDbContext<Database>(options =>
{
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), mySqlDbContextOptionsBuilder =>
    {
        mySqlDbContextOptionsBuilder.MigrationsAssembly("Netmon.AccountService");
    });
});

builder.Services.AddIdentityCore<User>()
    .AddEntityFrameworkStores<Database>()
    .AddApiEndpoints();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    
    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

WebApplication app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UsePathBase(new PathString("/api"));

app.UseCors("CorsPolicy");

app.MapGet("/authenticate", IResult (HttpContext context) =>
{
    if (context.User.Identity is not {IsAuthenticated: true})
    {
        return Results.Unauthorized();
    }
    return Results.Ok(new
    {
        context.User.Identity.IsAuthenticated,
        Id = context.User.FindFirstValue(ClaimTypes.NameIdentifier),
        context.User.Identity.Name,
        Email = context.User.FindFirstValue(ClaimTypes.Email),
    });
}).RequireAuthorization();

app.MapIdentityApi<User>();

app.UseAuthentication();
app.UseAuthorization();

using (IServiceScope scope = app.Services.CreateScope())
{
    Database database = scope.ServiceProvider.GetRequiredService<Database>();
    await database.Database.MigrateAsync();
}

app.Run();