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
using Netmon.Data.Write.Repositories.Component.Cpu;
using Netmon.Data.Write.Repositories.Component.Cpu.Core;
using Netmon.Data.Write.Repositories.Component.Disk;
using Netmon.Data.Write.Repositories.Component.Interface;
using Netmon.Data.Write.Repositories.Component.Memory;
using Netmon.Data.Write.Repositories.Device;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


MongoUrlBuilder mongoUrlBuilder = new("mongodb://root:mongopwd@192.168.178.8:27017/hangfire?authSource=admin");
if (builder.Environment.IsDevelopment())
{
    mongoUrlBuilder = new MongoUrlBuilder("mongodb://root:mongopwd@localhost:27017/hangfire?authSource=admin");
}
MongoClient mongoClient = new(mongoUrlBuilder.ToMongoUrl());

builder.Services.AddHangfire(configuration => configuration
    .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
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
    serverOptions.ServerName = "Hangfire.Mongo server 1";
});

string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DevicesDatabase>(options =>
{
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), o => o.MigrationsAssembly("Netmon.DeviceManager"));
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

WebApplication app = builder.Build();

app.UsePathBase(new PathString("/api"));

app.UseSwagger();
app.UseSwaggerUI();

app.UseHangfireDashboard("/hangfire", new DashboardOptions
{
    Authorization = new[]{ new HangfireAuthorizationFilter() }
});

app.MapControllers();

RecurringJob.AddOrUpdate("poll", () => Console.WriteLine("test"), "*/1 * * * *");

app.Run();