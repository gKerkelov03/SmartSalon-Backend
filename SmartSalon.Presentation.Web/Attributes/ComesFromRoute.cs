
namespace SmartSalon.Presentation.Web.Attributes;

public class ComesFromRouteAttribute(string name) : Attribute
{
    public string Name => name;
}