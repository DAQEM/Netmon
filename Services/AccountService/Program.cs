using AccountService.Database;
using Microsoft.EntityFrameworkCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AccountDatabase>(options =>
{
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

WebApplication app = builder.Build();

app.UsePathBase("/api");

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();