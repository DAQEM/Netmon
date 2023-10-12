using AspNetCore.Proxy;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient();
builder.Services.AddProxies();

WebApplication app = builder.Build();

app.UseRouting();

app.UseProxies(proxies =>
{
    proxies.Map("/api/account/{**remainder}", proxy =>
    {
        proxy.UseHttp((_, args) => GetHost(5001) + args["remainder"]);
    });
    proxies.Map("/api/device/{**remainder}", proxy =>
    {
        proxy.UseHttp((_, args) => GetHost(5002) + args["remainder"]);
    });
    proxies.Map("/api/polling/{**remainder}", proxy =>
    {
        proxy.UseHttp((_, args) => GetHost(5003) + args["remainder"]);
    });
});

app.Run();
return;

string GetHost(int port) => app.Environment.IsDevelopment() ? $"http://localhost:{port}/api/" : $"http://host.docker.internal:{port}/api/";