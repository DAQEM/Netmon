using System.Text.Json.Serialization;
using DevicesLib.Database;
using Microsoft.EntityFrameworkCore;
using SNMPPollingService.Entities.Component.Cpu;
using SNMPPollingService.Entities.Component.Disk;
using SNMPPollingService.Entities.Component.Interface;
using SNMPPollingService.Entities.Component.Memory;
using SNMPPollingService.SNMP.Converter.Component;
using SNMPPollingService.SNMP.Converter.Device;
using SNMPPollingService.SNMP.Manager;
using SNMPPollingService.SNMP.MIB.HostResources;
using SNMPPollingService.SNMP.MIB.If;
using SNMPPollingService.SNMP.MIB.System;
using SNMPPollingService.SNMP.MIB.UCDavis;
using SNMPPollingService.SNMP.Poll.Device;
using SNMPPollingService.SNMP.Poll.MIB;
using SNMPPollingService.SNMP.Poll.MIB.MIB;
using SNMPPollingService.SNMP.Poll.MIB.MIBs;

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

builder.Services.AddScoped<IDevicePoller, DevicePoller>();
builder.Services.AddScoped<IMIBsPoller, MIBsPoller>();
builder.Services.AddScoped<IMIBPoller<HostResourcesMIB>, HostResourcesMIBPoller>();
builder.Services.AddScoped<IMIBPoller<SystemMIB>, SystemMIBPoller>();
builder.Services.AddScoped<IMIBPoller<IfMIB>, IfMIBPoller>();
builder.Services.AddScoped<IMIBPoller<UCDavisMIB>, UCDavisMIBPoller>();
builder.Services.AddScoped<IMIBDeviceConverter, MIBDeviceConverter>();
builder.Services.AddScoped<IMIBComponentConverter<IDisk>, MIBDiskConverter>();
builder.Services.AddScoped<IMIBComponentConverter<IMemory>, MIBMemoryConverter>();
builder.Services.AddScoped<IMIBComponentConverter<ICpu>, MIBCpuConverter>();
builder.Services.AddScoped<IMIBComponentConverter<IInterface>, MIBInterfaceConverter>();

WebApplication app = builder.Build();

app.UsePathBase("/api");

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();