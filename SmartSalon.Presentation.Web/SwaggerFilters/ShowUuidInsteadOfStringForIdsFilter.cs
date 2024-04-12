using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SmartSalon.Presentation.Web.SwaggerFilters;

public class ShowUuidInsteadOfStringForIdsFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        //TODO: make the route parameters to show up as uuid in swagger not as string
        // foreach (var parameter in operation.Parameters)
        // {
        //     if (parameter.In == ParameterLocation.Path)
        //     {
        // parameter.Schema.Format = "uuid";
        // parameter.Schema.Type = "uuid";
        //     }
        // }
    }
}