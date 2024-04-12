using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.Extensions.Options;
using SmartSalon.Application.Abstractions.Lifetime;

namespace SmartSalon.Presentation.Web.Options;

public class MvcOptionsConfigurator : IConfigureOptions<MvcOptions>, ISingletonLifetime
{
    public void Configure(MvcOptions options)
    {
        options.ModelBinderProviders.Insert(0, new ObjectBinder());
        options.ModelBinderProviders.Insert(1, new IdBinder());
    }
}
