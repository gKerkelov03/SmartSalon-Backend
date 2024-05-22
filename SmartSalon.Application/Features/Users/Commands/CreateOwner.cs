using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Abstractions.MediatR;
using SmartSalon.Application.Domain.Salons;
using SmartSalon.Application.Domain.Users;
using SmartSalon.Application.Errors;
using SmartSalon.Application.Extensions;
using SmartSalon.Application.ResultObject;

namespace SmartSalon.Application.Features.Users.Commands;

public class CreateOwnerCommand : ICommand<CreateOwnerCommandResponse>, IMapTo<Owner>
{
    public Id SalonId { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string PhoneNumber { get; set; }
    public required string ProfilePictureUrl { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
}

public class CreateOwnerCommandResponse
{
    public Id CreatedOwnerId { get; set; }
}

internal class CreateOwnerCommandHandler(
    UsersManager _users,
    IEfRepository<Salon> _salons,
    IUnitOfWork _unitOfWork,
    IMapper _mapper
) : ICommandHandler<CreateOwnerCommand, CreateOwnerCommandResponse>
{
    public async Task<Result<CreateOwnerCommandResponse>> Handle(CreateOwnerCommand command, CancellationToken cancellationToken)
    {
        var userWithTheSameEmail = await _users.FindByEmailAsync(command.Email);

        if (userWithTheSameEmail is not null)
        {
            return Error.Conflict;
        }

        var salon = await _salons.All
            .Where(salon => salon.Id == command.SalonId)
            .Include(salon => salon.Owners)
            .FirstOrDefaultAsync();

        if (salon is null)
        {
            return Error.NotFound;
        }

        var newOwner = _mapper.Map<Owner>(command);
        newOwner.UserName = command.Email;

        salon.Owners!.Add(newOwner);

        var identityResultForCreation = await _users.CreateAsync(newOwner);
        var identityResultForAddingToRole = await _users.AddToRoleAsync(newOwner, OwnerRoleName);

        if (identityResultForCreation.Failure())
        {
            return new Error(identityResultForCreation.ErrorDescription());
        }

        if (identityResultForAddingToRole.Failure())
        {
            return new Error(identityResultForAddingToRole.ErrorDescription());
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new CreateOwnerCommandResponse { CreatedOwnerId = newOwner.Id };
    }
}
