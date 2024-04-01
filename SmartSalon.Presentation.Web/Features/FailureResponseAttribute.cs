
using Microsoft.AspNetCore.Mvc;
using SmartSalon.Presentation.Web.Features;

public class FailureResponseAttribute : ProducesResponseTypeAttribute
{
    public FailureResponseAttribute(int statusCode) : base(typeof(ProblemDetailsWithErrors), statusCode) { }
}