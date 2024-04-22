
using Asp.Versioning;

namespace SmartSalon.Presentation.Web.Attributes;

[AttributeUsage(AttributeTargets.Class, Inherited = true)]
public class APIVersionAttribute : ApiVersionAttribute
{
    public APIVersionAttribute(int version) : base(version) { }
}