using System.ComponentModel.DataAnnotations;
using SmartSalon.Application.Commands;
using SmartSalon.Application.Mapping;

namespace SmartSalon.Presentation.Web.Models.Requests;

public class CreateRequest : IMapTo<CreateCommand>
{
    [Required]
    public string Name { get; set; }

    [Required]
    public int? Age { get; set; }
}