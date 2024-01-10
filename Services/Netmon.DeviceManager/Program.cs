using Netmon.DeviceManager.Filters;
using Netmon.Data.EntityFramework.Database;
using Hangfire;
using Hangfire.Mongo;
using Hangfire.Mongo.Migration.Strategies;
using Hangfire.Mongo.Migration.Strategies.Backup;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
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
using Netmon.DeviceManager.Jobs.Poll;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

string? connectionString = builder.Configuration["MySQL:ConnectionString"];

builder.Services.AddDbContext<DevicesDatabase>(options =>
{
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        corsPolicyBuilder => corsPolicyBuilder.WithOrigins(builder.Configuration["Cors:AllowedOrigins"]?.Split(",") ?? Array.Empty<string>())
            .AllowAnyMethod()
            .AllowAnyHeader());
});

MongoUrlBuilder mongoUrlBuilder = new(builder.Configuration["MongoDB:ConnectionString"]);
MongoClient mongoClient = new(mongoUrlBuilder.ToMongoUrl());

builder.Services.AddHangfire(configuration => configuration
    .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UseFilter(new AutomaticRetryAttribute { Attempts = 0 })
    .UseMongoStorage(mongoClient, mongoUrlBuilder.DatabaseName, new MongoStorageOptions
    {
        MigrationOptions = new MongoMigrationOptions
        {
            MigrationStrategy = new MigrateMongoMigrationStrategy(),
            BackupStrategy = new CollectionMongoBackupStrategy(),
        },
        Prefix = "hangfire.mongo",
        CheckConnection = true,
        CheckQueuedJobsStrategy = CheckQueuedJobsStrategy.TailNotificationsCollection
    })
);

builder.Services.AddHangfireServer(serverOptions =>
{
    serverOptions.ServerName = builder.Configuration["Hangfire:ServerName"];
});

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

builder.Services.AddScoped<IPollDeviceJob, PollDeviceJob>();

WebApplication app = builder.Build();

WebSocketOptions webSocketOptions = new()
{
    KeepAliveInterval = TimeSpan.FromSeconds(30)
};

app.UseWebSockets(webSocketOptions);

app.UsePathBase(new PathString("/api"));

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("CorsPolicy");

app.UseHangfireDashboard(builder.Configuration["Hangfire:Endpoint"], new DashboardOptions
{
    Authorization = new[]{ new HangfireAuthorizationFilter() }
});

app.MapControllers();

using (IServiceScope scope = app.Services.CreateScope())
{
    try
    {
        IPollDeviceJob pollDeviceJob = scope.ServiceProvider.GetRequiredService<IPollDeviceJob>();
        RecurringJob.AddOrUpdate(
            builder.Configuration["Hangfire:PollingTask:Name"], 
            () => pollDeviceJob.Execute(), 
            builder.Configuration["Hangfire:PollingTask:Cron"]);
    }
    catch (InvalidOperationException e)
    {
        Console.WriteLine("Unable to start polling job (probably because it cannot connect to the database): {0}", e.Message);
    }

    
}

app.Run();