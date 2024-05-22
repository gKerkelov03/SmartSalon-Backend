using System.Reflection;
using Microsoft.OpenApi.Models;
using SmartSalon.Application.Extensions;
using Swashbuckle.AspNetCore.SwaggerGen;
using SmartSalon.Presentation.Web.Attributes;

namespace SmartSalon.Presentation.Web.SwaggerFilters;

public class HideParametersWithComesFromRouteAttributeFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        var excludedProperties = context.Type.GetProperties()
            .Where(property => property.GetCustomAttribute<ComesFromRouteAttribute>() != null)
            .Select(property => property.Name);

        foreach (var propertyName in excludedProperties)
        {
            if (schema.Properties.ContainsKey(propertyName) || schema.Properties.ContainsKey(propertyName.WithFirstLetterToLower()))
            {
                schema.Properties.Remove(propertyName);
                schema.Properties.Remove(propertyName.WithFirstLetterToLower());
            }
        }
    }
}