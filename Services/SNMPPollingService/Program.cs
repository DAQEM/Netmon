using System.Text.Json.Serialization;
using DevicesLib.Database;
using Microsoft.EntityFrameworkCore;
using SNMPPollingService.SNMP.Manager;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options => 
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<DevicesDatabase>(options =>
{
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), 
        mySqlDbContextOptionsBuilder => mySqlDbContextOptionsBuilder.MigrationsAssembly("DeviceManagerService"));
});

builder.Services.AddScoped<ISNMPManager, SNMPManager>();

WebApplication app = builder.Build();

app.UsePathBase("/api");

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();