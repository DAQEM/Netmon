using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using Netmon.Data.EntityFramework.Database;
using Netmon.Data.EntityFramework.Read.Repositories.Component.Cpu;
using Netmon.Data.EntityFramework.Read.Repositories.Component.Cpu.Core;
using Netmon.Data.EntityFramework.Read.Repositories.Component.Disk;
using Netmon.Data.EntityFramework.Read.Repositories.Component.Interface;
using Netmon.Data.EntityFramework.Read.Repositories.Component.Memory;
using Netmon.Data.EntityFramework.Read.Repositories.Device;
using Netmon.Data.Repositories.Read.Component.Cpu;
using Netmon.Data.Repositories.Read.Component.Cpu.Core;
using Netmon.Data.Repositories.Read.Component.Disk;
using Netmon.Data.Repositories.Read.Component.Interface;
using Netmon.Data.Repositories.Read.Component.Memory;
using Netmon.Data.Repositories.Read.Device;
using Netmon.Data.Repositories.Write.Component.Cpu;
using Netmon.Data.Repositories.Write.Component.Cpu.Core;
using Netmon.Data.Repositories.Write.Component.Disk;
using Netmon.Data.Repositories.Write.Component.Interface;
using Netmon.Data.Repositories.Write.Component.Memory;
using Netmon.Data.Repositories.Write.Device;
using Netmon.Data.Services.Read.Component.Cpu;
using Netmon.Data.Services.Read.Component.Cpu.Core;
using Netmon.Data.Services.Read.Component.Disk;
using Netmon.Data.Services.Read.Component.Interface;
using Netmon.Data.Services.Read.Component.Memory;
using Netmon.Data.Services.Read.Device;
using Netmon.Data.Services.Read.Services.Component.Cpu;
using Netmon.Data.Services.Read.Services.Component.Cpu.Core;
using Netmon.Data.Services.Read.Services.Component.Disk;
using Netmon.Data.Services.Read.Services.Component.Interface;
using Netmon.Data.Services.Read.Services.Component.Memory;
using Netmon.Data.Services.Read.Services.Device;
using Netmon.Data.Services.Write.Component.Cpu;
using Netmon.Data.Services.Write.Component.Cpu.Core;
using Netmon.Data.Services.Write.Component.Disk;
using Netmon.Data.Services.Write.Component.Interface;
using Netmon.Data.Services.Write.Component.Memory;
using Netmon.Data.Services.Write.Device;
using Netmon.Data.Services.Write.Services.Component.Cpu;
using Netmon.Data.Services.Write.Services.Component.Cpu.Core;
using Netmon.Data.Services.Write.Services.Component.Disk;
using Netmon.Data.Services.Write.Services.Component.Interface;
using Netmon.Data.Services.Write.Services.Component.Memory;
using Netmon.Data.Services.Write.Services.Device;
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
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
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

builder.Services.AddScoped<IDeviceReadRepository, DeviceReadRepository>();
builder.Services.AddScoped<IDeviceConnectionReadRepository, DeviceConnectionReadRepository>();
builder.Services.AddScoped<IDiskReadRepository, DiskReadRepository>();
builder.Services.AddScoped<IDiskMetricReadRepository, DiskMetricsReadRepository>();
builder.Services.AddScoped<IInterfaceReadRepository, InterfaceReadRepository>();
builder.Services.AddScoped<IInterfaceMetricReadRepository, InterfaceMetricsReadRepository>();
builder.Services.AddScoped<IMemoryReadRepository, MemoryReadRepository>();
builder.Services.AddScoped<IMemoryMetricReadRepository, MemoryMetricsReadRepository>();
builder.Services.AddScoped<ICpuReadRepository, CpuReadRepository>();
builder.Services.AddScoped<ICpuMetricReadRepository, CpuMetricsReadRepository>();
builder.Services.AddScoped<ICpuCoreReadRepository, CpuCoreReadRepository>();
builder.Services.AddScoped<ICpuCoreMetricReadRepository, CpuCoreMetricsReadRepository>();

builder.Services.AddScoped<IDeviceWriteService, DeviceWriteService>();
builder.Services.AddScoped<IDeviceConnectionWriteService, DeviceConnectionWriteService>();
builder.Services.AddScoped<IDiskWriteService, DiskWriteService>();
builder.Services.AddScoped<IDiskMetricsWriteService, DiskMetricsWriteService>();
builder.Services.AddScoped<IInterfaceWriteService, InterfaceWriteService>();
builder.Services.AddScoped<IInterfaceMetricsWriteService, InterfaceMetricsWriteService>();
builder.Services.AddScoped<IMemoryWriteService, MemoryWriteService>();
builder.Services.AddScoped<IMemoryMetricsWriteService, MemoryMetricsWriteService>();
builder.Services.AddScoped<ICpuWriteService, CpuWriteService>();
builder.Services.AddScoped<ICpuMetricsWriteService, CpuMetricsWriteService>();
builder.Services.AddScoped<ICpuCoreWriteService, CpuCoreWriteService>();
builder.Services.AddScoped<ICpuCoreMetricsWriteService, CpuCoreMetricsWriteService>();

builder.Services.AddScoped<IDeviceReadService, DeviceReadService>();
builder.Services.AddScoped<IDeviceConnectionReadService, DeviceConnectionReadService>();
builder.Services.AddScoped<IDiskReadService, DiskReadService>();
builder.Services.AddScoped<IDiskMetricReadService, DiskMetricsReadService>();
builder.Services.AddScoped<IInterfaceReadService, InterfaceReadService>();
builder.Services.AddScoped<IInterfaceMetricReadService, InterfaceMetricsReadService>();
builder.Services.AddScoped<IMemoryReadService, MemoryReadService>();
builder.Services.AddScoped<IMemoryMetricReadService, MemoryMetricsReadService>();
builder.Services.AddScoped<ICpuReadService, CpuReadService>();
builder.Services.AddScoped<ICpuMetricReadService, CpuMetricsReadService>();
builder.Services.AddScoped<ICpuCoreReadService, CpuCoreReadService>();
builder.Services.AddScoped<ICpuCoreMetricReadService, CpuCoreMetricsReadService>();

WebApplication app = builder.Build();

app.UsePathBase("/api");

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

if (!app.Environment.IsDevelopment())
{
    app.UseMiddleware<ExceptionMiddleware>();
}

using (IServiceScope serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
{
    DevicesDatabase? context = serviceScope.ServiceProvider.GetService<DevicesDatabase>();
    try
    {
        context?.Database.Migrate();
    }
    catch (MySqlException _)
    {
    }
}

app.Run();


namespace Netmon.SNMPPolling
{
    public class SNMPPollingProgram { }
}