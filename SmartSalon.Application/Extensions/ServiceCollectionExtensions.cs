using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SmartSalon.Application.PipelineBehaviors;

namespace SmartSalon.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        => services
            .RegisterMediatR()
            .RegisterValidators()
            .RegisterRedis(config);

    internal static IServiceCollection RegisterMediatR(this IServiceCollection services)
        => services.AddMediatR(options => options
            .RegisterServicesFromAssembly(typeof(IApplicationLayerMarker).Assembly)
            .AddOpenBehavior(typeof(LoggingPipelineBehaviour<,>))
            .AddOpenBehavior(typeof(ValidationPipelineBehavior<,>))
            .AddOpenBehavior(typeof(CachingPipelineBehaviour<,>))
        );

    internal static IServiceCollection RegisterRedis(this IServiceCollection services, IConfiguration configuration)
        => services.AddStackExchangeRedisCache(options =>
            options.Configuration = configuration.GetConnectionString("Redis")
        );

    internal static IServiceCollection RegisterValidators(this IServiceCollection services)
        => services.AddValidatorsFromAssembly(
            typeof(IApplicationLayerMarker).Assembly,
            includeInternalTypes: true
        );
}
