﻿using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Abstractions.MediatR;
using SmartSalon.Application.Domain.Users;
using SmartSalon.Application.Errors;
using SmartSalon.Application.Extensions;
using SmartSalon.Application.ResultObject;

namespace SmartSalon.Application.Features.Users.Commands;

public class UpdateUserCommand : ICommand, IMapTo<User>
{
    public Id UserId { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string ProfilePictureUrl { get; set; }
    public required string PhoneNumber { get; set; }
}

internal class UpdateCommandHandler(UsersManager _users)
    : ICommandHandler<UpdateUserCommand>
{
    public async Task<Result> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
    {
        var user = await _users.FindByIdAsync(command.UserId.ToString());
        user.MapAgainst(command);
        var identityResult = await _users.UpdateAsync(user);

        if (identityResult.Failure())
        {
            return new Error(identityResult.ErrorDescription());
        }

        return Result.Success();
    }
}
