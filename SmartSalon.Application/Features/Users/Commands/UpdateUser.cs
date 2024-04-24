using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Abstractions.MediatR;
using SmartSalon.Application.Domain.Users;
using SmartSalon.Application.Errors;
using SmartSalon.Application.Extensions;
using SmartSalon.Application.ResultObject;

namespace SmartSalon.Application.Features.Users.Commands;

public class UpdateUserCommand : ICommand
{
    public Id UserId { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string ProfilePictureUrl { get; set; }
    public required string PhoneNumber { get; set; }
}

internal class UpdateCommandHandler(IEfRepository<User> users, IUnitOfWork _unitOfWork)
    : ICommandHandler<UpdateUserCommand>
{
    public async Task<Result> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
    {
        var user = await users.GetByIdAsync(command.UserId);

        if (user is null)
        {
            return Error.NotFound;
        }

        user.MapAgainst(command);
        users.Update(user);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
