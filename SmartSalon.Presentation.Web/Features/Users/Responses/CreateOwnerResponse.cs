
using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Features.Users.Commands;

namespace SmartSalon.Presentation.Web.Features.Users.Responses;

public class CreateOwnerResponse : IMapFrom<AddWorkerToSalonCommandResponse>
{
    public required Id CreatedOwnerId { get; set; }
}

