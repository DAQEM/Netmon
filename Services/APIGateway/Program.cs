using Microsoft.AspNetCore.Mvc;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient();

WebApplication app = builder.Build();

async Task ForwardRequest(HttpContext context, string url)
{
   HttpClient client = context.RequestServices.GetRequiredService<HttpClient>();
   string? remainder = context.Request.RouteValues["remainder"] as string;
   HttpResponseMessage response = await client.GetAsync(url + remainder);
   if (!response.IsSuccessStatusCode)
   {
      context.Response.StatusCode = (int)response.StatusCode;
      return;
   }
   await context.Response.WriteAsync(await response.Content.ReadAsStringAsync());
}

string GetHost(int port) => app.Environment.IsDevelopment() ? $"http://localhost:{port}/api/" : $"http://host.docker.internal:{port}/api/";

app.MapGet("/api/account/{*remainder}", async context => {
   await ForwardRequest(context, GetHost(5001));
});

app.MapGet("/api/device/{*remainder}", async context => {
   await ForwardRequest(context, GetHost(5002));
});

app.MapGet("/api/polling/{*remainder}", async context => {
   await ForwardRequest(context, GetHost(5003));
});



app.Run();