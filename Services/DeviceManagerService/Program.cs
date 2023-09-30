using DevicesLib.Database;
using Microsoft.EntityFrameworkCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DevicesDatabase>(options =>
{
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), o => o.MigrationsAssembly("DeviceManagerService"));
});

WebApplication app = builder.Build();

app.UsePathBase(new PathString("/api"));

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();