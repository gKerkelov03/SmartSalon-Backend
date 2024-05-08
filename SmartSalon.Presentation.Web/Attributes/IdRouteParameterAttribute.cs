
namespace SmartSalon.Presentation.Web.Attributes;

//TODO: refactor the app to use [FromRoute(IdRouteParameterName)] instead of this attribute
[AttributeUsage(AttributeTargets.Property)]
public class IdRouteParameterAttribute : Attribute
{
}