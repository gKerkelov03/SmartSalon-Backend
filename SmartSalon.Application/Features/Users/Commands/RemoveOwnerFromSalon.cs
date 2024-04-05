using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Abstractions.MediatR;
using SmartSalon.Application.Domain;
using SmartSalon.Application.Domain.Users;
using SmartSalon.Application.Errors;
using SmartSalon.Application.ResultObject;

namespace SmartSalon.Application.Features.Users.Commands;

public class RemoveOwnerFromSalonCommand : ICommand
{
    public Id OwnerId { get; set; }
    public Id SalonId { get; set; }
}

internal class RemoveOwnerFromSalonCommandHandler(
    IEfRepository<Owner> _owners,
    IEfRepository<Salon> _salons,
    IUnitOfWork _unitOfWork
) : ICommandHandler<RemoveOwnerFromSalonCommand>
{
    public async Task<Result> Handle(RemoveOwnerFromSalonCommand command, CancellationToken cancellationToken)
    {
        var owner = await _owners.GetByIdAsync(command.OwnerId);
        var salon = _salons.All
            .Where(salon => salon.Id == command.SalonId)
            .Include(salon => salon.Owners)
            .FirstOrDefault();

        if (owner is null || salon is null)
        {
            return Error.NotFound;
        }

        salon?.Owners?.Add(owner);
        await _unitOfWork.SaveAsync(cancellationToken);

        return Result.Success();
    }
}
