using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SmartSalon.Application.Options;
using SmartSalon.Application.PipelineBehaviors;
using SmartSalon.Application.ResultObject;

namespace SmartSalon.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration config)
        => services
            .RegisterMediatR()
            .RegisterValidators()
            .RegisterRedis(config);

    internal static IServiceCollection RegisterMediatR(this IServiceCollection services)
        => services.AddMediatR(options => options
            .RegisterServicesFromAssembly(typeof(IResult).Assembly)
            .AddOpenBehavior(typeof(LoggingPipelineBehaviour<,>))
            .AddOpenBehavior(typeof(ValidationPipelineBehavior<,>))
            .AddOpenBehavior(typeof(CachingPipelineBehaviour<,>))
        );

    internal static IServiceCollection RegisterRedis(this IServiceCollection services, IConfiguration config)
        => services.AddStackExchangeRedisCache(options =>
            options.Configuration = config.GetConnectionString(nameof(ConnectionStringOptions.Redis))
        );

    internal static IServiceCollection RegisterValidators(this IServiceCollection services)
        => services.AddValidatorsFromAssembly(typeof(IResult).Assembly, includeInternalTypes: true);
}
