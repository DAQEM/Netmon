using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Netmon.Data.EntityFramework.Database;
using Netmon.Data.Repositories.Write.Component.Cpu;
using Netmon.Data.Repositories.Write.Component.Cpu.Core;
using Netmon.Data.Repositories.Write.Component.Disk;
using Netmon.Data.Repositories.Write.Component.Interface;
using Netmon.Data.Repositories.Write.Component.Memory;
using Netmon.Data.Repositories.Write.Device;
using Netmon.Data.Write.Repositories.Component.Cpu;
using Netmon.Data.Write.Repositories.Component.Cpu.Core;
using Netmon.Data.Write.Repositories.Component.Disk;
using Netmon.Data.Write.Repositories.Component.Interface;
using Netmon.Data.Write.Repositories.Component.Memory;
using Netmon.Data.Write.Repositories.Device;
using Netmon.Models.Component.Cpu;
using Netmon.Models.Component.Disk;
using Netmon.Models.Component.Interface;
using Netmon.Models.Component.Memory;
using Netmon.SNMPPolling.Middleware;
using Netmon.SNMPPolling.SNMP.Converter.Component;
using Netmon.SNMPPolling.SNMP.Converter.Device;
using Netmon.SNMPPolling.SNMP.Manager;
using Netmon.SNMPPolling.SNMP.MIB.HostResources;
using Netmon.SNMPPolling.SNMP.MIB.If;
using Netmon.SNMPPolling.SNMP.MIB.System;
using Netmon.SNMPPolling.SNMP.MIB.UCDavis;
using Netmon.SNMPPolling.SNMP.Poll.Device;
using Netmon.SNMPPolling.SNMP.Poll.MIB.MIB;
using Netmon.SNMPPolling.SNMP.Poll.MIB.MIBs;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options => 
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string? connectionString = builder.Configuration["MySQL:ConnectionString"];

builder.Services.AddDbContext<DevicesDatabase>(options =>
{
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), 
        mySqlDbContextOptionsBuilder => mySqlDbContextOptionsBuilder.MigrationsAssembly("Netmon.DeviceManager"));
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

builder.Services.AddScoped<IDeviceWriteRepository, DeviceWriteRepository>();
builder.Services.AddScoped<IDeviceConnectionWriteRepository, DeviceConnectionWriteRepository>();
builder.Services.AddScoped<IDiskWriteRepository, DiskWriteRepository>();
builder.Services.AddScoped<IDiskMetricsWriteRepository, DiskMetricsWriteRepository>();
builder.Services.AddScoped<IInterfaceWriteRepository, InterfaceWriteRepository>();
builder.Services.AddScoped<IInterfaceMetricsWriteRepository, InterfaceMetricsWriteRepository>();
builder.Services.AddScoped<IMemoryWriteRepository, MemoryWriteRepository>();
builder.Services.AddScoped<IMemoryMetricsWriteRepository, MemoryMetricsWriteRepository>();
builder.Services.AddScoped<ICpuWriteRepository, CpuWriteRepository>();
builder.Services.AddScoped<ICpuMetricsWriteRepository, CpuMetricsWriteRepository>();
builder.Services.AddScoped<ICpuCoreWriteRepository, CpuCoreWriteRepository>();
builder.Services.AddScoped<ICpuCoreMetricsWriteRepository, CpuCoreMetricsWriteRepository>();

WebApplication app = builder.Build();

app.UsePathBase("/api");

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

if (!app.Environment.IsDevelopment())
{
    app.UseMiddleware<ExceptionMiddleware>();
}

app.Run();


namespace Netmon.SNMPPolling
{
    public partial class SNMPPollingProgram { }
}