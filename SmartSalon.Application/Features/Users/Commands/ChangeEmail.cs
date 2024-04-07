﻿using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Abstractions.MediatR;
using SmartSalon.Application.Errors;
using SmartSalon.Application.Extensions;
using SmartSalon.Application.ResultObject;

namespace SmartSalon.Application.Features.Users.Commands;

public class ChangeEmailCommand : ICommand
{
    public Id UserId { get; set; }
    public required string NewEmail { get; set; }
    public required string Password { get; set; }
}

internal class ChangeEmailCommandHandler(UsersManager _usersManager, ICurrentUserAccessor _currentUser)
    : ICommandHandler<ChangeEmailCommand>
{
    public async Task<Result> Handle(ChangeEmailCommand command, CancellationToken cancellationToken)
    {
        if (_currentUser.Id != command.UserId)
        {
            return Error.Unauthorized;
        }

        var user = await _usersManager.FindByIdAsync(command.UserId.ToString());

        if (user is null)
        {
            return Error.NotFound;
        }

        var token = await _usersManager.GenerateChangeEmailTokenAsync(user, command.NewEmail);
        var identityResult = await _usersManager.ChangeEmailAsync(user, command.NewEmail, token);

        if (identityResult.Failure())
        {
            return new Error(identityResult.ErrorDescription());
        }

        return Result.Success();
    }
}
