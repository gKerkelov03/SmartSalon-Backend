using SmartSalon.Application.Abstractions.MediatR;
using SmartSalon.Application.Errors;
using SmartSalon.Application.ResultObject;

namespace SmartSalon.Application.Features.Users.Commands;

public class DeleteUserCommand : ICommand
{
    public Id UserId { get; set; }

    public DeleteUserCommand(Id userId) => UserId = userId;
}

internal class DeleteUserCommandHandler(UsersManager _usersManager)
    : ICommandHandler<DeleteUserCommand>
{
    public async Task<Result> Handle(DeleteUserCommand command, CancellationToken cancellationToken)
    {
        var user = await _usersManager.FindByIdAsync(command.UserId.ToString());

        if (user is null)
        {
            return Error.NotFound;
        }

        await _usersManager.DeleteAsync(user);

        return await Task.FromResult(Result.Success());
    }
}
