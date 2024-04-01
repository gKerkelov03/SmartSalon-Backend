﻿using System.ComponentModel.DataAnnotations;
using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Commands;

namespace SmartSalon.Presentation.Web.Features.Users.Requests;

public class UpdateUserRequest : IMapTo<UpdateUserCommand>
{
    [Required]
    public Id UserId { get; set; }

    [Required]
    public required string Nickname { get; set; }

    [Required]
    public required string FirstName { get; set; }

    [Required]
    public required string LastName { get; set; }

    [Required]
    public required string PictureUrl { get; set; }
}