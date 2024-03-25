using System.ComponentModel.DataAnnotations;
using SmartSalon.Application.Mapping;
using SmartSalon.Application.Queries;

namespace SmartSalon.Presentation.Web.Models.Requests;

public class CreateRequest : IMapTo<CreateCommand>
{
    [Required]
    public required string Name { get; set; }

    [Required]
    public required int Age { get; set; }
}