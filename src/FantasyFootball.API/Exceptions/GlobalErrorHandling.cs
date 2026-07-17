using System.Net;
using System.Text.Json;

namespace FantasyFootball.API.Exceptions;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(
        RequestDelegate next,
        ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);

            context.Response.ContentType = "application/json";

            var response = new
            {
                StatusCode = (int)HttpStatusCode.InternalServerError,
                Message = ex.Message,
                Errors = new List<string>()
            };

            if (ex is FluentValidation.ValidationException validationEx)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                response = new
                {
                    StatusCode = context.Response.StatusCode,
                    Message = "Validation failed",
                    Errors = validationEx.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }
            else if (ex is FantasyFootball.Domain.Exceptions.DomainException domainEx)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                response = new
                {
                    StatusCode = context.Response.StatusCode,
                    Message = domainEx.Message,
                    Errors = new List<string>()
                };
            }
            else
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }

            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}