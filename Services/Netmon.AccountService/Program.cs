using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Netmon.AccountService;
using Netmon.AccountService.Extensions;
using Swashbuckle.AspNetCore.Filters;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        corsPolicyBuilder => corsPolicyBuilder.WithOrigins(builder.Configuration["Cors:AllowedOrigins"]?.Split(",") ?? Array.Empty<string>())
            .AllowAnyMethod()
            .AllowAnyHeader()
        );
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

app.MapIdentityApi<User>();
app.MapMyIdentityApi();

app.UseAuthentication();
app.UseAuthorization();

using (IServiceScope scope = app.Services.CreateScope())
{
    Database database = scope.ServiceProvider.GetRequiredService<Database>();
    if (database.Database.GetPendingMigrations().Any())
    {
        database.Database.Migrate();
    }
}

app.Run();