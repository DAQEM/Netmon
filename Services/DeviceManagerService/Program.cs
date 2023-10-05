using DeviceManagerService.Filters;
using DevicesLib.Database;
using Hangfire;
using Hangfire.Mongo;
using Hangfire.Mongo.Migration.Strategies;
using Hangfire.Mongo.Migration.Strategies.Backup;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

MongoUrlBuilder mongoUrlBuilder = new("mongodb://root:mongopwd@192.168.178.8:27017/hangfire?authSource=admin");
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
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), o => o.MigrationsAssembly("DeviceManagerService"));
});

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