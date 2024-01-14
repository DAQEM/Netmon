using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Netmon.AccountService;
using Swashbuckle.AspNetCore.Filters;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        corsPolicyBuilder => corsPolicyBuilder.WithOrigins(builder.Configuration["Cors:AllowedOrigins"]?.Split(",") ?? Array.Empty<string>())
            .AllowAnyMethod()
            .AllowAnyHeader());
});

builder.Services.AddAuthentication().AddBearerToken(IdentityConstants.BearerScheme);
builder.Services.AddAuthorizationBuilder();
        
string connectionString = builder.Configuration["MySQL:Identity:ConnectionString"] ?? throw new InvalidOperationException("MySQL connection string is not configured.");

builder.Services.AddDbContext<Database>(options =>
{
    options.UseMySQL(connectionString, builder =>
    {
        builder.MigrationsAssembly("Netmon.AccountService");
    });
});

builder.Services.AddIdentityCore<User>()
    .AddEntityFrameworkStores<Database>()
    .AddApiEndpoints();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
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


app.UseCors("CorsPolicy");

app.MapControllers();

app.MapGet("/authenticate", IResult () => Results.Ok()).RequireAuthorization();
app.MapIdentityApi<User>();

using (IServiceScope scope = app.Services.CreateScope())
{
    Database database = scope.ServiceProvider.GetRequiredService<Database>();
    await database.Database.MigrateAsync();
}

app.Run();