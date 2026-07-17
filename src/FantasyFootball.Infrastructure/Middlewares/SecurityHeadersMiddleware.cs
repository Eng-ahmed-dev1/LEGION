namespace FantasyFootball.Infrastructure.Middlewares;

public class SecurityHeadersMiddleware
{
    private readonly RequestDelegate _next;

    public SecurityHeadersMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Prevent browsers from MIME-sniffing a response away from the declared content-type
        context.Response.Headers.Append("X-Content-Type-Options", "nosniff");
        
        // Prevent clickjacking by not allowing the site to be framed
        context.Response.Headers.Append("X-Frame-Options", "DENY");
        
        // Enable XSS protection in the browser
        context.Response.Headers.Append("X-XSS-Protection", "1; mode=block");
        
        // Prevent browsers from sending the Referer header to other sites
        context.Response.Headers.Append("Referrer-Policy", "strict-origin-when-cross-origin");
        
        // Enforce HTTPS
        context.Response.Headers.Append("Strict-Transport-Security", "max-age=31536000; includeSubDomains");

        // Basic Content Security Policy (CSP)
        context.Response.Headers.Append("Content-Security-Policy", "default-src 'self'; script-src 'self' 'unsafe-inline'; style-src 'self' 'unsafe-inline';");

        await _next(context);
    }
}
