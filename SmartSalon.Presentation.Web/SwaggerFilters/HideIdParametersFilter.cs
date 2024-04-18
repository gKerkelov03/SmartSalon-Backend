using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SmartSalon.Presentation.Web.SwaggerFilters;

public class HideIdParametersFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var parametersToRemove = operation.Parameters
            .Where(p => p.Schema.Type == "string" && p.Schema.Format == "uuid")
            .ToList();

        foreach (var parameter in parametersToRemove)
        {
            operation.Parameters.Remove(parameter);
        }
    }
}