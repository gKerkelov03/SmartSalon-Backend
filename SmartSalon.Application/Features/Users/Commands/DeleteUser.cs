using MediatR;
using SmartSalon.Application.Abstractions.MediatR;
using SmartSalon.Application.Notifications;
using SmartSalon.Application.ResultObject;

namespace SmartSalon.Application.Commands;

public class DeleteUserCommand : ICommand
{
    public required Id UserId { get; set; }
}

internal class DeleteUserCommandHandler(UsersManager _usersManager, IPublisher _publisher)
    : ICommandHandler<DeleteUserCommand>
{
    public async Task<Result> Handle(DeleteUserCommand command, CancellationToken cancellationToken)
    {
        await _publisher.Publish(new UserChangedNotification() { Id = command.UserId });

        return await Task.FromResult(Result.Success());
    }
}
