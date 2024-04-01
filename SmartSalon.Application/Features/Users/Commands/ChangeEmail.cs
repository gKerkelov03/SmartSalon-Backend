using MediatR;
using SmartSalon.Application.Abstractions.MediatR;
using SmartSalon.Application.ResultObject;

namespace SmartSalon.Application.Commands;

public class ChangeEmailCommand : ICommand
{
    public Id UserId { get; set; }

    public required string NewEmail { get; set; }

    public required string Password { get; set; }
}

internal class ChangeEmailCommandHandler(UsersManager usersManager, IPublisher _publisher)
    : ICommandHandler<ChangeEmailCommand>
{
    public async Task<Result> Handle(ChangeEmailCommand command, CancellationToken cancellationToken)
    {
        return await Task.FromResult(Result.Success());
    }
}
