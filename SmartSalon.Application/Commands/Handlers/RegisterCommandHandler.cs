using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Commands.Responses;
using SmartSalon.Application.Domain;
using SmartSalon.Application.Errors;
using SmartSalon.Application.Extensions;
using SmartSalon.Application.ResultObject;

namespace SmartSalon.Application.Commands.Handlers;

internal class RegisterCommandHandler(UsersManager _usersManager)
    : ICommandHandler<RegisterCommand, RegisterCommandResponse>
{
    public async Task<Result<RegisterCommandResponse>> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        var userToCreate = command.MapTo<User>();
        var identityResult = await _usersManager.CreateAsync(userToCreate);

        if (!identityResult.Succeeded)
        {
            return Error.Conflict(identityResult.GetErrorMessage());
        }

        return new RegisterCommandResponse() { Id = userToCreate.Id };
    }
}
