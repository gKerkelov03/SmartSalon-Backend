using System.ComponentModel.DataAnnotations;
using SmartSalon.Application.Queries;
using SmartSalon.Shared.Mapping.Abstractions;

namespace SmartSalon.Presentation.Web.Models.Requests;

public class ExampleRequest : IMapTo<ExampleQuery>
{
    [Required]
    public required string ExampleProperty1 { get; set; }

    [Required]
    public required int ExampleProperty2 { get; set; }
}