using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Abstractions.MediatR;
using SmartSalon.Application.Domain;
using SmartSalon.Application.Errors;
using SmartSalon.Application.Extensions;
using SmartSalon.Application.ResultObject;

namespace SmartSalon.Application.Commands;

public class RegisterCommand : ICommand<RegisterCommandResponse>, IMapTo<User>
{
    public required string UserName { get; set; }

    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    public required string Email { get; set; }

    public required string Password { get; set; }

    public required string PictureUrl { get; set; }
}

public class RegisterCommandResponse
{
    public Id Id { get; set; }
}

internal class RegisterCommandHandler(UsersManager _usersManager)
    : ICommandHandler<RegisterCommand, RegisterCommandResponse>
{
    public async Task<Result<RegisterCommandResponse>> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        var userToCreate = command.MapTo<User>();

        userToCreate.UserName = $"{command.FirstName} {command.LastName}";

        var identityResult = await _usersManager.CreateAsync(userToCreate);

        if (!identityResult.Succeeded)
        {
            return Error.Conflict(identityResult.GetErrorMessage());
        }

        return new RegisterCommandResponse() { Id = userToCreate.Id };
    }
}
