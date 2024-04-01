﻿
using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Commands;

namespace SmartSalon.Presentation.Web.Features.Users.Responses;

public class LoginResponse : IMapFrom<LoginCommandResponse>
{
    public required string Jwt { get; set; }
}
