using Serilog.Context;
using SmartSalon.Application.Abstractions;

namespace SmartSalon.Presentation.Web;

public class RequestLogContextMiddleware : IMiddleware, ISingletonLifetime
{
    public Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var correlationId = "CorrelationId";

        using (LogContext.PushProperty(correlationId, context.TraceIdentifier))
        {
            return next(context);
        }
    }
}
