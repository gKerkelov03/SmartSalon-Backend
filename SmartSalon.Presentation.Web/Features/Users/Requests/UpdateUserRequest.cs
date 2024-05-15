using Microsoft.AspNetCore.Mvc;
using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Features.Users.Commands;
using SmartSalon.Presentation.Web.Attributes;

namespace SmartSalon.Presentation.Web.Features.Users.Requests;

public class UpdateUserRequest : IMapTo<UpdateUserCommand>
{
    [ComesFromRoute(IdRouteParameterName)]
    public Id UserId { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string ProfilePictureUrl { get; set; }
    public required string PhoneNumber { get; set; }
}