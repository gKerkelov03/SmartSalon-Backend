using System.Collections;
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

    private static object ConvertToType(Type targetType, ModelBindingContext bindingContext, string propertyName, object propertyValue)
    {
        object convertedValue;

        if (targetType == typeof(Id))
        {
            convertedValue = ConvertToId(bindingContext, propertyName, propertyValue);
        }
        else if (targetType == typeof(DateTime))
        {
            convertedValue = ConvertToDateTime(bindingContext, propertyName, propertyValue); ;
        }
        else if (targetType.IsAssignableTo(typeof(Enum)))
        {
            convertedValue = ConvertToEnum(bindingContext, targetType, propertyName, propertyValue);
        }
        else if (targetType == typeof(TimeOnly))
        {
            convertedValue = ConvertToTimeOnly(bindingContext, propertyName, propertyValue);
        }
        else if (targetType == typeof(DateOnly))
        {
            convertedValue = ConvertToTimeOnly(bindingContext, propertyName, propertyValue);
        }
        else if (targetType == typeof(IEnumerable<Id>))
        {
            convertedValue = ConvertToListOfIds(bindingContext, propertyName, propertyValue);
        }
        else
        {
            convertedValue = Convert.ChangeType(propertyValue, targetType);
        }

        return convertedValue;
    }

    private static DateTimeOffset ConvertToDateTime(ModelBindingContext bindingContext, string propertyName, object propertyValue)
    {
        var propertyValueAsString = propertyValue.CastTo<string>();
        var isNotValidDateTime = !DateTime.TryParse(propertyValueAsString, out var dateTime);

        if (isNotValidDateTime)
        {
            bindingContext.ModelState.TryAddModelError(propertyName, "Invalid format");
        }

        return dateTime;
    }

    private static TimeOnly ConvertToTimeOnly(ModelBindingContext bindingContext, string propertyName, object propertyValue)
    {
        var propertyValueAsString = propertyValue.CastTo<string>();
        var isNotValidTime = !TimeOnly.TryParse(propertyValueAsString, out var timeOnly);

        if (isNotValidTime)
        {
            bindingContext.ModelState.TryAddModelError(propertyName, "Invalid format");
        }

        return timeOnly;
    }

    private static DateOnly ConvertToDateOnly(ModelBindingContext bindingContext, string propertyName, object propertyValue)
    {
        var propertyValueAsString = propertyValue.CastTo<string>();
        var isNotValidDate = !DateOnly.TryParse(propertyValueAsString, out var timeOnly);

        if (isNotValidDate)
        {
            bindingContext.ModelState.TryAddModelError(propertyName, "Invalid format");
        }

        return timeOnly;
    }

    private static List<Id> ConvertToListOfIds(ModelBindingContext bindingContext, string propertyName, object propertyValue)
    {
        var result = new List<Id>();

        foreach (var id in propertyValue.CastTo<IEnumerable>())
        {
            result.Add(ConvertToId(bindingContext, propertyName, id.ToString()));
        }

        return result;
    }

    private static object ConvertToEnum(ModelBindingContext bindingContext, Type enumType, string propertyName, object propertyValue)
    {
        var propertyValueAsString = propertyValue.CastTo<string>();
        var isNumber = int.TryParse(propertyValueAsString, out var _);

        if (isNumber)
        {
            bindingContext.ModelState.TryAddModelError(propertyName, "Invalid format");
        }

        var convertedValue = Enum.Parse(enumType, propertyValueAsString);

        return convertedValue;
    }

    public IModelBinder? GetBinder(ModelBinderProviderContext context)
        => context.Metadata.IsComplexType ? this : null;
}