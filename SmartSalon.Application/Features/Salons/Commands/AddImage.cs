using Microsoft.EntityFrameworkCore;
using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Abstractions.MediatR;
using SmartSalon.Application.Domain.Salons;
using SmartSalon.Application.Errors;
using SmartSalon.Application.ResultObject;

namespace SmartSalon.Application.Features.Salons.Commands;

public class AddImageCommand : ICommand, IMapTo<Salon>
{
    public required string Url { get; set; }
    public Id SalonId { get; set; }
}

internal class AddImageCommandHandler(IEfRepository<Salon> _salons, IUnitOfWork _unitOfWork)
    : ICommandHandler<AddImageCommand>
{
    public async Task<Result> Handle(AddImageCommand command, CancellationToken cancellationToken)
    {
        var salon = await _salons.All
            .Include(salon => salon.Images)
            .Where(salon => salon.Id == command.SalonId)
            .FirstOrDefaultAsync();

        var newImage = new Image { SalonId = command.SalonId, Url = command.Url };

        if (salon is null)
        {
            return Error.NotFound;
        }

        salon.Images!.Add(newImage);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
