using Microsoft.AspNetCore.Mvc;

public class SuccessResponseAttribute : ProducesResponseTypeAttribute
{
    public SuccessResponseAttribute(int statusCode) : base(statusCode) { }
}

public class SuccessResponseAttribute<TResponse> : ProducesResponseTypeAttribute
{
    public SuccessResponseAttribute(int statusCode) : base(typeof(TResponse), statusCode) { }
}