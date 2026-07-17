namespace Fantasy.API.Middlewares;

public sealed class GlobalExceptionHandler(
    ILogger<GlobalExceptionHandler> logger)
    : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        logger.LogError(
            exception,
            "An unhandled exception occurred. TraceId: {TraceId}",
            httpContext.TraceIdentifier);

        var response = new ProblemDetails
        {
            Title = "Internal Server Error",
            Detail = "An unexpected error occurred.",
            Status = StatusCodes.Status500InternalServerError,
            Instance = httpContext.Request.Path
        };

        httpContext.Response.StatusCode = response.Status!.Value;

        await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);

        return true;
    }
}