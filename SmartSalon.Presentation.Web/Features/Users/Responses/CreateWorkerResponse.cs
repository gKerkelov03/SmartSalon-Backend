﻿
using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Commands;

namespace SmartSalon.Presentation.Web.Features.Users.Responses;

public class CreateWorkerResponse : IMapFrom<CreateWorkerCommandResponse>
{
    public required Id CreatedWorkerId { get; set; }
}

