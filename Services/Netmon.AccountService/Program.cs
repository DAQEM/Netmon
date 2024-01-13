using Microsoft.EntityFrameworkCore;
using Netmon.Identity;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        corsPolicyBuilder => corsPolicyBuilder.WithOrigins(builder.Configuration["Cors:AllowedOrigins"]?.Split(",") ?? Array.Empty<string>())
            .AllowAnyMethod()
            .AllowAnyHeader());
});

builder.Services.AddNetmonIdentityWithApiEndpoints(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

WebApplication app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("CorsPolicy");

app.UserNetmonIdentityWithApiEndpoints();

using (IServiceScope scope = app.Services.CreateScope())
{
    Database database = scope.ServiceProvider.GetRequiredService<Database>();
    await database.Database.MigrateAsync();
}

app.Run();