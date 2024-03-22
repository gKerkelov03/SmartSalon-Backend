using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using SmartSalon.Application.Behaviors;

namespace SmartSalon.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        var thisAssembly = typeof(IApplicationLayerMarker).Assembly;

        return services
            .AddMediatR(options => options
                .RegisterServicesFromAssembly(thisAssembly)
                .AddOpenBehavior(typeof(LoggingPipelineBehaviour<,>))
                .AddOpenBehavior(typeof(ValidationPipelineBehavior<,>))
            )
            .AddValidatorsFromAssembly(
                thisAssembly,
                includeInternalTypes: true
            );
    }
}
