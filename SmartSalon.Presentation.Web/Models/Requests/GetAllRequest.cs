using System.ComponentModel.DataAnnotations;
using SmartSalon.Application.Mapping;
using SmartSalon.Application.Queries;

namespace SmartSalon.Presentation.Web.Models.Requests;

public class GetAllRequest : IMapTo<GetAllQuery>
{
    // [Required]
    public required string SearchTerm { get; set; }
}