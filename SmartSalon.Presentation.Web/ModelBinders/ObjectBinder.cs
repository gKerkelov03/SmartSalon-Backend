using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using SmartSalon.Application.Extensions;

internal class ObjectBinder(IdConverter _idConverter) : IModelBinder, IModelBinderProvider
{
    public async Task BindModelAsync(ModelBindingContext bindingContext)
    {
        var requestBodyMap = await GetRequestBodyMapAsync(bindingContext);

        if (requestBodyMap is null)
        {
            return;
        }

        var requestedIdRouteParameter = bindingContext.ActionContext.RouteData.Values[IdRouteParameterName]?.ToString();
        var requestModel = Activator.CreateInstance(bindingContext.ModelType)!;

        foreach (var property in requestModel.GetType().GetProperties())
        {
            var jsonPropertyName = property.Name.WithFirstLetterToLower();
            var fromRouteAttribute = property.GetCustomAttribute<FromRouteAttribute>(inherit: false);

            if (fromRouteAttribute is not null && fromRouteAttribute.Name == IdRouteParameterName)
            {
                if (requestedIdRouteParameter is null)
                {
                    bindingContext.ModelState.AddModelError(jsonPropertyName, $"No route value passed for parameter {jsonPropertyName}");
                    return;
                }

                var id = _idConverter.Convert(bindingContext, jsonPropertyName, requestedIdRouteParameter);
                requestModel!.GetType().GetProperty(property.Name)!.SetValue(requestModel, id);
            }
            else if (!requestBodyMap.ContainsKey(jsonPropertyName))
            {
                bindingContext.ModelState.AddModelError(jsonPropertyName, $"Property {jsonPropertyName} is required");
                return;
            }
            else
            {
                object convertedValue = new ObjectConverter(property.PropertyType)
                    .Convert(bindingContext, jsonPropertyName, requestBodyMap[jsonPropertyName]);

                if (!bindingContext.ModelState.IsValid)
                {
                    return;
                }

                requestModel.GetType().GetProperty(property.Name)!.SetValue(requestModel, convertedValue);
            }
        }

        bindingContext.Result = ModelBindingResult.Success(requestModel);
    }

    private static async Task<IDictionary<string, object>?> GetRequestBodyMapAsync(ModelBindingContext bindingContext)
    {
        try
        {
            var request = bindingContext.HttpContext.Request;
            request.EnableBuffering();

            using var requestBodyReader = new StreamReader(request.Body, Encoding.UTF8, leaveOpen: true);
            var bodyAsText = await requestBodyReader.ReadToEndAsync();
            request.Body.Position = 0;

            return JsonConvert.DeserializeObject<Dictionary<string, object>>(bodyAsText)!;
        }
        catch (Exception ex)
        {
            bindingContext.ModelState.AddModelError("$", ex.Message);
            return null;
        }
    }

    public IModelBinder? GetBinder(ModelBinderProviderContext context)
        => context.Metadata.IsComplexType ? this : null;
}