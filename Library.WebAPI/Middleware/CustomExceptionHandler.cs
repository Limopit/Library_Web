using System.Net;
using System.Text.Json;
using Library.Application.Common.Exceptions;

namespace Library.WebAPI.Middleware;

public class CustomExceptionHandler
{
    private readonly RequestDelegate _requestDelegate;

    public CustomExceptionHandler(RequestDelegate request) => _requestDelegate = request;

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _requestDelegate(context);
        }
        catch (Exception e)
        {
            await HandleExceptionAsync(context, e);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception e)
    {
        var code = HttpStatusCode.InternalServerError;
        var result = string.Empty;
        switch (e)
        {
            case CustomValidationException сve:
                code = HttpStatusCode.BadRequest;
                result = JsonSerializer.Serialize(new
                {
                    Errors = сve.errors
                });
                break;
            case NotFoundException nfe:
                code = HttpStatusCode.NotFound;
                break;
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;

        if (result == string.Empty)
        {
            result = JsonSerializer.Serialize(new { err = e.Message });
        }

        return context.Response.WriteAsync(result);
    }
}