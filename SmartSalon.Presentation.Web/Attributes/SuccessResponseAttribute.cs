using Microsoft.AspNetCore.Mvc;

namespace SmartSalon.Presentation.Web.Attributes;

public class SuccessResponseAttribute : ProducesResponseTypeAttribute
{
    public SuccessResponseAttribute(int statusCode) : base(statusCode) { }
}

public class SuccessResponseAttribute<TResponse> : ProducesResponseTypeAttribute
{
    public SuccessResponseAttribute(int statusCode) : base(typeof(TResponse), statusCode) { }
}