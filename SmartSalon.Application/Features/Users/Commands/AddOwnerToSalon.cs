using Microsoft.EntityFrameworkCore;
using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Abstractions.MediatR;
using SmartSalon.Application.Domain;
using SmartSalon.Application.Domain.Users;
using SmartSalon.Application.Errors;
using SmartSalon.Application.ResultObject;

namespace SmartSalon.Application.Features.Users.Commands;

public class AddOwnerToSalonCommand : ICommand
{
    public Id SalonId { get; set; }
    public Id OwnerId { get; set; }
}

internal class AddOwnerToSalonCommandHandler(IEfRepository<Owner> _owners, IEfRepository<Salon> _salons, IUnitOfWork _unitOfWork)
    : ICommandHandler<AddOwnerToSalonCommand>
{
    public async Task<Result> Handle(AddOwnerToSalonCommand command, CancellationToken cancellationToken)
    {
        var owner = await _owners.GetByIdAsync(command.OwnerId);
        var salon = await _salons.All
            .Where(salon => salon.Id == command.SalonId)
            .Include(salon => salon.Owners)
            .FirstOrDefaultAsync();

        if (owner is null || salon is null)
        {
            return Error.NotFound;
        }

        salon.Owners!.Add(owner);
        await _unitOfWork.SaveAsync(cancellationToken);

        return Result.Success();
    }
}
