using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace SmartSalon.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var thisAssembly = typeof(IApplicationLayerMarker).Assembly;

        return services
                .AddMediatR(options => options
                    .RegisterServicesFromAssembly(thisAssembly)
                )
                .AddValidatorsFromAssembly(
                    thisAssembly,
                    includeInternalTypes: true
                );
    }
}
