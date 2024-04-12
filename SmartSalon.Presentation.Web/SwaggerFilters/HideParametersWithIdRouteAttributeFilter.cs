using System.Reflection;
using Microsoft.OpenApi.Models;
using SmartSalon.Presentation.Web.Attributes;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SmartSalon.Presentation.Web.SwaggerFilters;

public class HideParametersWithIdRouteAttributeFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        var excludedProperties = context.Type.GetProperties()
            .Where(property => property.GetCustomAttribute<IdRouteParameterAttribute>() != null)
            .Select(property => property.Name);

        foreach (var propertyName in excludedProperties)
        {
            if (schema.Properties.ContainsKey(propertyName))
            {
                schema.Properties.Remove(propertyName);
            }
        }
    }
}