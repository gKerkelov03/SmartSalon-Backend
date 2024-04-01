﻿
using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Commands;

namespace SmartSalon.Presentation.Web.Features.Users.Responses;

public class CreateOwnerResponse : IMapFrom<CreateWorkerCommandResponse>
{
    public required Id CreatedOwnerId { get; set; }
}
