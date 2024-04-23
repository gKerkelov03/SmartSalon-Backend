using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Features.Users.Queries;

namespace SmartSalon.Presentation.Web.Features.Salons.Responses;

public class GetSalonByIdResponse : IMapFrom<GetOwnerByIdQueryResponse>
{
    public Id OwnerId { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public bool IsEmailConfirmed { get; set; }
    public required string ProfilePictureUrl { get; set; }
    public required string PhoneNumber { get; set; }
    public required IEnumerable<Id> SalonsOwned { get; set; }
}