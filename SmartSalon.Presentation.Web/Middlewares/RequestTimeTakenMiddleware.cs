using System.Diagnostics;
using Microsoft.AspNetCore.Http;

public class RequestTimingMiddleware(RequestDelegate _next, ILogger<RequestTimingMiddleware> _logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        var stopwatch = new Stopwatch();
        stopwatch.Start();

        await _next(context);

        stopwatch.Stop();
        var elapsedMilliseconds = stopwatch.ElapsedMilliseconds;

        _logger.LogInformation($"Request {context.Request.Method} {context.Request.Path} took {elapsedMilliseconds} ms");
    }
}
