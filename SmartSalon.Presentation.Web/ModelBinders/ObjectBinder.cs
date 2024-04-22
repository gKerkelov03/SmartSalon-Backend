using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using SmartSalon.Application.Extensions;
using SmartSalon.Presentation.Web.Attributes;

public class ObjectBinder : BaseBinder, IModelBinder, IModelBinderProvider
{
    public async Task BindModelAsync(ModelBindingContext context)
    {
        var requestBodyMap = await GetRequestBodyMapAsync(context);

        if (requestBodyMap is null)
        {
            return;
        }

        var IdRouteParameterPassed = GetTheIdRouteParameter(context);
        var requestModel = Activator.CreateInstance(context.ModelType)!;

        foreach (var property in requestModel.GetType().GetProperties())
        {
            var jsonPropertyName = property.Name.WithFirstLetterToLower();
            var hasIdRouteParameterAttribute = property.GetCustomAttributes(typeof(IdRouteParameterAttribute)).Any();

            if (hasIdRouteParameterAttribute)
            {
                if (IdRouteParameterPassed is null)
                {
                    context.ModelState.AddModelError(jsonPropertyName, $"No route value passed for parameter {jsonPropertyName}");
                    return;
                }

                var id = ConvertToId(context, jsonPropertyName, IdRouteParameterPassed);
                requestModel!.GetType().GetProperty(property.Name)!.SetValue(requestModel, id);
            }
            else if (!requestBodyMap.ContainsKey(jsonPropertyName))
            {
                context.ModelState.AddModelError(jsonPropertyName, $"Property {jsonPropertyName} is required");
                return;
            }
            else
            {
                object convertedValue = ConvertToType(property.PropertyType, context, jsonPropertyName, requestBodyMap[jsonPropertyName]);

                if (!context.ModelState.IsValid)
                {
                    return;
                }

                requestModel.GetType().GetProperty(property.Name)!.SetValue(requestModel, convertedValue);
            }
        }

        context.Result = ModelBindingResult.Success(requestModel);
    }

    private static async Task<IDictionary<string, string>?> GetRequestBodyMapAsync(ModelBindingContext bindingContext)
    {
        try
        {
            var request = bindingContext.HttpContext.Request;
            request.EnableBuffering();

            using var requestBodyReader = new StreamReader(request.Body, Encoding.UTF8, leaveOpen: true);
            var bodyAsText = await requestBodyReader.ReadToEndAsync();
            request.Body.Position = 0;

            return JsonConvert.DeserializeObject<Dictionary<string, string>>(bodyAsText)!;
        }
        catch (Exception ex)
        {
            bindingContext.ModelState.AddModelError("$", ex.Message);
            return null;
        }
    }

    private static object ConvertToType(Type targetType, ModelBindingContext bindingContext, string propertyName, string propertyValue)
    {
        object convertedValue;

        if (targetType == typeof(Id))
        {
            convertedValue = ConvertToId(bindingContext, propertyName, propertyValue);
        }
        else if (targetType == typeof(DateTimeOffset))
        {
            convertedValue = ConvertToDateTimeOffset(bindingContext, propertyName, propertyValue);
        }
        else if (targetType == typeof(DateTime))
        {
            convertedValue = ConvertToDateTime(bindingContext, propertyName, propertyValue); ;
        }
        else
        {
            convertedValue = Convert.ChangeType(propertyValue, targetType);
        }

        return convertedValue;
    }

    private static DateTimeOffset ConvertToDateTimeOffset(ModelBindingContext bindingContext, string propertyName, string propertyValue)
    {
        var isValidDateTimeOffset = DateTimeOffset.TryParse(propertyValue, out var dateTimeOffset);

        if (!isValidDateTimeOffset)
        {
            bindingContext.ModelState.TryAddModelError(propertyName, "Invalid format");
        }

        return dateTimeOffset;
    }

    private static DateTimeOffset ConvertToDateTime(ModelBindingContext bindingContext, string propertyName, string propertyValue)
    {
        var isValidDateTime = DateTime.TryParse(propertyValue, out var dateTime);

        if (!isValidDateTime)
        {
            bindingContext.ModelState.TryAddModelError(propertyName, "Invalid format");
        }

        return dateTime;
    }

    public IModelBinder? GetBinder(ModelBinderProviderContext context)
        => context.Metadata.IsComplexType ? this : null;
}