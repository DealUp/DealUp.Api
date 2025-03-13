using System.Net;
using DealUp.Exceptions;
using DealUp.Infrastructure.Configuration;
using DealUp.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace DealUp.Infrastructure.Middlewares;

public class ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
{
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        logger.LogError(exception, "Exception: {ExceptionMessage}", exception.Message);

        ExceptionData error = GetException(exception);
        if (!context.Response.HasStarted)
        {
            context.Response.StatusCode = error.StatusCode;
        }

        context.Response.ContentType = "application/json";
        return context.Response.WriteAsync(error.ToJson());
    }

    private static ExceptionData GetException(Exception ex)
    {
        return ex.ToExceptionData();
    }
}

internal static class ExceptionResponseMiddlewareHelper
{
    internal static ExceptionData ToExceptionData(this Exception exception)
    {
        var statusCode = HttpStatusCode.InternalServerError;
        object? additionalData = null;
        if (exception is ResponseErrorException responseException)
        {
            additionalData = responseException.AdditionalData;
            statusCode = responseException.ResponseStatusCode;
        }
        
        return new ExceptionData((int)statusCode, exception.Message, additionalData);
    }
}