using System.ComponentModel.DataAnnotations;
using SmartSalon.Application.Mapping;
using SmartSalon.Application.Queries;

namespace SmartSalon.Presentation.Web.Models.Requests;

public class ExampleRequest : IMapTo<ExampleQuery>, IMapTo<ExampleCommand>
{
    [Required]
    public required string ExampleProperty1 { get; set; }

    [Required]
    public required int ExampleProperty2 { get; set; }
}