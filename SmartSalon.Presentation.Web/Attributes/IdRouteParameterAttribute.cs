using Microsoft.AspNetCore.Mvc;

namespace SmartSalon.Presentation.Web.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class IdRouteParameterAttribute : ApiExplorerSettingsAttribute
{
    public IdRouteParameterAttribute()
    {
        this.IgnoreApi = true;
    }
}