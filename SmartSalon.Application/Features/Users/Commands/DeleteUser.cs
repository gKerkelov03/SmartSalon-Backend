using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Abstractions.MediatR;
using SmartSalon.Application.Domain.Users;
using SmartSalon.Application.Errors;
using SmartSalon.Application.ResultObject;

namespace SmartSalon.Application.Features.Users.Commands;

public class DeleteUserCommand(Id userId) : ICommand
{
    public Id UserId => userId;
}

internal class DeleteUserCommandHandler(IEfRepository<User> _users, IUnitOfWork _unitOfWork)
    : ICommandHandler<DeleteUserCommand>
{
    public async Task<Result> Handle(DeleteUserCommand command, CancellationToken cancellationToken)
    {
        var user = await _users.GetByIdAsync(command.UserId);

        if (user is null)
        {
            return Error.NotFound;
        }

        await _users.RemoveByIdAsync(user.Id);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return await Task.FromResult(Result.Success());
    }
}
