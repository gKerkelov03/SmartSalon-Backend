using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SmartSalon.Application.Abstractions.Lifetime;

namespace SmartSalon.Presentation.Web.OptionsConfigurators;

public class ModelBindersConfigurator : IConfigureOptions<MvcOptions>, ITransientLifetime
{
    public void Configure(MvcOptions options)
    {
        options.ModelBinderProviders.Insert(0, new ObjectBinder());
        options.ModelBinderProviders.Insert(1, new IdBinder());
    }
}
