using System.Net;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;

namespace Netmon.DeviceManager.Middleware;

public class AuthorizationMiddleware(IHttpClientFactory httpClientFactory) : IAuthorizationMiddlewareResultHandler
{
    public async Task HandleAsync(RequestDelegate next, HttpContext context, AuthorizationPolicy policy,
        PolicyAuthorizationResult authorizeResult)
    {
        string? authorizationHeader = context.Request.Headers.Authorization;

        if (AuthenticationHeaderValue.TryParse(authorizationHeader, out AuthenticationHeaderValue? headerValue))
        {
            string? accountServiceUrl = context.RequestServices.GetRequiredService<IConfiguration>().GetSection("AccountService")["Url"];
            string authenticateUrl = $"{accountServiceUrl}/authenticate";

            HttpClient httpClient = httpClientFactory.CreateClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", headerValue.Parameter);
            HttpResponseMessage response = await httpClient.GetAsync(authenticateUrl);
        
            if (response.StatusCode == HttpStatusCode.OK)
            {
                await next(context);
                return;
            }
        }
        
        context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
    }
}