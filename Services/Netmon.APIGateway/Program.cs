using AspNetCore.Proxy;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient();
builder.Services.AddProxies();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
    {
        builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

WebApplication app = builder.Build();

app.UseRouting();

app.UseCors("CorsPolicy");

app.UseProxies(proxies =>
{
    proxies.Map("/api/account/{**remainder}", proxy =>
    {
        proxy.UseHttp((context, args) => GetHost(5001) + args["remainder"] + context.Request.QueryString);
    });
    proxies.Map("/api/device/{**remainder}", proxy =>
    {
        proxy.UseHttp((context, args) => GetHost(5002) + args["remainder"] + context.Request.QueryString);
    });
    proxies.Map("/api/polling/{**remainder}", proxy =>
    {
        proxy.UseHttp((context, args) => GetHost(5003) + args["remainder"] + context.Request.QueryString);
    });
});

app.Run();
return;

string GetHost(int port) => app.Environment.IsDevelopment() ? $"http://localhost:{port}/api/" : GetContainerHost(port);

string GetContainerHost(int port)
{
    return port switch
    {
        5001 => "http://netmon-account-service:80/api/",
        5002 => "http://netmon-device-manager-service:80/api/",
        5003 => "http://netmon-snmp-polling-service:80/api/",
        _ => ""
    };
}

