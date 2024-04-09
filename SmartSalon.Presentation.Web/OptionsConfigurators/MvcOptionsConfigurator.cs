using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace SmartSalon.Presentation.Web.Options;

public class MvcOptionsConfigurator : IConfigureOptions<MvcOptions>
{
    public void Configure(MvcOptions options)
        => options.ModelBinderProviders.Insert(0, new IdModelBinder());
}
