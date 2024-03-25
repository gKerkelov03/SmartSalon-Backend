using System.ComponentModel.DataAnnotations;

namespace SmartSalon.Presentation.Web.Models.Requests;

public class UpdateRequest
{
    [Required]
    public required Id Id { get; set; }

    [Required]
    public required string Name { get; set; }

    [Required]
    public required int Age { get; set; }
}