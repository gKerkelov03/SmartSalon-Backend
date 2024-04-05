using SmartSalon.Application.Abstractions.MediatR;
using SmartSalon.Application.Domain.Users;
using SmartSalon.Application.Errors;
using SmartSalon.Application.Extensions;
using SmartSalon.Application.ResultObject;

namespace SmartSalon.Application.Features.Users.Commands;

public class UpdateUserCommand : ICommand
{
    public Id UserId { get; set; }
    public required string UserName { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string PictureUrl { get; set; }
}

internal class UpdateCommandHandler(IEfRepository<User> _users)
    : ICommandHandler<UpdateUserCommand>
{
    public async Task<Result> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
    {
        var result = await _users.UpdateByIdAsync(command.UserId, command.MapTo<User>());

        if (result.IsFailure)
        {
            return Error.NotFound;
        }

        return Result.Success();
    }
}
