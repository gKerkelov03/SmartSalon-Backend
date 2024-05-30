using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Features.Users.Queries;

namespace SmartSalon.Presentation.Web.Features.Users.Responses;

public class GetOwnerByIdResponse : IMapFrom<GetOwnerByIdQueryResponse>
{
    public Id Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public bool EmailConfirmed { get; set; }
    public required string ProfilePictureUrl { get; set; }
    public required string PhoneNumber { get; set; }
    public required IEnumerable<Id> Salons { get; set; }
    public required IEnumerable<string> Roles { get; set; }
}