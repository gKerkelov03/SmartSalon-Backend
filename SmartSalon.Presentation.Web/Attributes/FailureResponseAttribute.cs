
using Microsoft.AspNetCore.Mvc;
using SmartSalon.Presentation.Web.Models;

namespace SmartSalon.Presentation.Web.Attributes;

public class FailureResponseAttribute : ProducesResponseTypeAttribute
{
    public FailureResponseAttribute(int statusCode) : base(typeof(ProblemDetailsWithErrors), statusCode) { }
}