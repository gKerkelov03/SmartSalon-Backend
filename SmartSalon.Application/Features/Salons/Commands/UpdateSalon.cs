using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Abstractions.MediatR;
using SmartSalon.Application.Domain.Salons;
using SmartSalon.Application.Errors;
using SmartSalon.Application.Extensions;
using SmartSalon.Application.ResultObject;

namespace SmartSalon.Application.Features.Salons.Commands;

public class UpdateSalonCommand : ICommand
{
    public required Id SalonId { get; set; }
    public required Id MainCurrencyId { get; set; }
    public required string Name { get; set; }
    public required string GoogleMapsLocation { get; set; }
    public required string Latitude { get; set; }
    public required string Longitude { get; set; }
    public required string Description { get; set; }
    public string? ProfilePictureUrl { get; set; }
    public required int TimePenalty { get; set; }
    public required int BookingsInAdvance { get; set; }
    public bool SubscriptionsEnabled { get; set; }
    public bool SectionsEnabled { get; set; }
    public bool WorkersCanMoveBookings { get; set; }
    public bool WorkersCanSetNonWorkingPeriods { get; set; }
}

internal class UpdateSalonCommandHandler(IEfRepository<Salon> _salons, IUnitOfWork _unitOfWork)
    : ICommandHandler<UpdateSalonCommand>
{
    public async Task<Result> Handle(UpdateSalonCommand command, CancellationToken cancellationToken)
    {
        var salon = await _salons.GetByIdAsync(command.SalonId);

        if (salon is null)
        {
            return Error.NotFound;
        }

        salon.MapAgainst(command);
        _salons.Update(salon);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
