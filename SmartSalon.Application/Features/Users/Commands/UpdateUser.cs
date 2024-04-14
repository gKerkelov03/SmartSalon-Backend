using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Abstractions.MediatR;
using SmartSalon.Application.Domain.Users;
using SmartSalon.Application.Errors;
using SmartSalon.Application.Extensions;
using SmartSalon.Application.ResultObject;

namespace SmartSalon.Application.Features.Users.Commands;

public class UpdateUserCommand : ICommand, IMapTo<User>
{
    public Id UserId { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string ProfilePictureUrl { get; set; }
    public required string PhoneNumber { get; set; }
}

internal class UpdateCommandHandler(UsersManager _users, IEfRepository<User> users, IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateUserCommand>
{
    public async Task<Result> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
    {
        // var user = await _users.FindByIdAsync(command.UserId.ToString());

        // if (user is null)
        // {
        //     return Error.NotFound;
        // }

        // user.MapAgainst(command);
        // var identityResult = await _users.UpdateAsync(user);

        // if (identityResult.Failure())
        // {
        //     return new Error(identityResult.ErrorDescription());
        // }

        // return Result.Success();
        var user = await users.GetByIdAsync(command.UserId);

        if (user is null)
        {
            return Error.NotFound;
        }

        user.MapAgainst(command);
        users.Update(user);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
