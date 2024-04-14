using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SmartSalon.Application.Extensions;
using SmartSalon.Presentation.Web.Attributes;

public class ObjectBinder : IModelBinder, IModelBinderProvider
{
    public async Task BindModelAsync(ModelBindingContext bindingContext)
    {
        using StreamReader requestBodyReader = new(bindingContext.HttpContext.Request.Body, Encoding.UTF8);
        var body = await requestBodyReader.ReadToEndAsync();

        var requestBodyMap = JsonConvert.DeserializeObject<Dictionary<string, string>>(body)!;

        var IdRouteParameterName = IdRoute.Remove(['{', '}']);
        var routeValue = bindingContext.ActionContext.RouteData.Values[IdRouteParameterName]?.ToString();
        var requestModel = Activator.CreateInstance(bindingContext.ModelType)!;

        foreach (var property in requestModel.GetType().GetProperties())
        {
            var propertyName = property.Name.WithFirstLetterToLower();
            var hasIdRouteParameterAttribute = property.GetCustomAttributes(typeof(IdRouteParameterAttribute)).Any();

            if (hasIdRouteParameterAttribute)
            {
                if (routeValue is null)
                {
                    bindingContext.ModelState.AddModelError(propertyName, $"No route value passed for parameter {propertyName}");
                }

                var isValidGuid = Guid.TryParse(routeValue, out var id);

                if (isValidGuid)
                {
                    requestModel!.GetType().GetProperty(property.Name)!.SetValue(requestModel, id);
                }
            }
            else
            {
                if (!requestBodyMap.ContainsKey(propertyName))
                {
                    throw new Exception();
                }

                object convertedValue;
                if (property.PropertyType == typeof(Id))
                {
                    convertedValue = requestBodyMap[propertyName].ToId();
                }
                else if (property.PropertyType == typeof(DateTimeOffset))
                {
                    convertedValue = DateTimeOffset.Parse(requestBodyMap[propertyName]);
                }
                else
                {
                    convertedValue = Convert.ChangeType(requestBodyMap[propertyName], property.PropertyType);
                }

                requestModel.GetType().GetProperty(property.Name)!.SetValue(requestModel, convertedValue);
            }
        }

        bindingContext.Result = ModelBindingResult.Success(requestModel);
    }

    public IModelBinder? GetBinder(ModelBinderProviderContext context)
        => context.Metadata.IsComplexType ? this : null;
}