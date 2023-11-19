using System.Net;
using System.Text.Json;
using DistrictSales.Api.Domain.Exceptions;

namespace DistrictSales.Api.Middleware;

public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public GlobalExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ObjectNotFoundException exception)
        {
            await HandleExceptionAsync(context, exception, HttpStatusCode.NotFound);
        }
        catch (DuplicateEntryException exception)
        {
            await HandleExceptionAsync(context, exception, HttpStatusCode.BadRequest);
        }
        catch (Exception exception)
        {
            await HandleExceptionAsync(context, exception, HttpStatusCode.InternalServerError);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception, HttpStatusCode httpStatusCode)
    {
        var response = context.Response;
        response.ContentType = "application/json";
        context.Response.StatusCode = (int)httpStatusCode;

        string result = JsonSerializer.Serialize(new { message = exception.Message });

        await response.WriteAsync(result);
    }
}
