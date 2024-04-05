
using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Features.Users.Commands;

namespace SmartSalon.Presentation.Web.Features.Users.Responses;

public class CreateWorkerResponse : IMapFrom<AddWorkerToSalonCommandResponse>
{
    public required Id CreatedWorkerId { get; set; }
}

