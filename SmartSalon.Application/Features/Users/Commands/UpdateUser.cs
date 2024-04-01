using MediatR;
using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Abstractions.MediatR;
using SmartSalon.Application.Features.Users.Notifications;
using SmartSalon.Application.ResultObject;

namespace SmartSalon.Application.Features.Users.Commands;

public class UpdateUserCommand : ICommand
{
    public required Id Id { get; set; }

    public required string Name { get; set; }

    public required int Age { get; set; }
}

internal class UpdateCommandHandler(IUnitOfWork _unitOfWork, UsersManager _usersManager, IPublisher _publisher)
    : ICommandHandler<UpdateUserCommand>
{
    public async Task<Result> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
    {
        await _publisher.Publish(new UserChangedNotification() { Id = command.Id });

        return await Task.FromResult(Result.Success());
    }
}
