using MediatR;
using SmartSalon.Application.Abstractions.MediatR;
using SmartSalon.Application.Features.Users.Notifications;
using SmartSalon.Application.ResultObject;

namespace SmartSalon.Application.Features.Users.Commands;

public class ChangePasswordCommand : ICommand
{
    public Id UserId { get; set; }

    public Id CurrentPassword { get; set; }

    public Id NewPassword { get; set; }
}

internal class ChangePasswordCommandHandler(UsersManager _usersManager, IPublisher _publisher)
    : ICommandHandler<ChangePasswordCommand>
{
    public async Task<Result> Handle(ChangePasswordCommand command, CancellationToken cancellationToken)
    {
        await _publisher.Publish(new UserChangedNotification() { Id = command.UserId });

        return await Task.FromResult(Result.Success());
    }
}
