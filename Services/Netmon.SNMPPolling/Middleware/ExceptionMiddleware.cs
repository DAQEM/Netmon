using System.Net;
using Netmon.SNMPPolling.SNMP.Exception;

namespace Netmon.SNMPPolling.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (System.Exception ex)
        {
            await HandleExceptionAsync(context, ex);
            Console.WriteLine(ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, System.Exception exception)
    {
        ExceptionResult result;
        
        if (exception is BaseException baseException)
        {
            result = ExceptionHandler.HandleException(baseException);
        }
        else
        {
            result = new ExceptionResult(
                exception.Message,
                HttpStatusCode.InternalServerError.GetHashCode()
            );
        }

        context.Response.StatusCode = result.StatusCode;
        return context.Response.WriteAsJsonAsync(result);
    }
}