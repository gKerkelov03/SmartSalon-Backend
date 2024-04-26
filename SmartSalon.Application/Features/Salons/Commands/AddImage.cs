using Microsoft.EntityFrameworkCore;
using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Abstractions.MediatR;
using SmartSalon.Application.Domain.Salons;
using SmartSalon.Application.Errors;
using SmartSalon.Application.ResultObject;

namespace SmartSalon.Application.Features.Salons.Commands;

public class AddImageCommand : ICommand<AddImageCommandResponse>
{
    public required string Url { get; set; }
    public Id SalonId { get; set; }
}

public class AddImageCommandResponse(Id id)
{
    public Id CreatedImageId => id;
}

internal class AddImageCommandHandler(
    IEfRepository<Salon> _salons,
    IEfRepository<Image> _images,
    IUnitOfWork _unitOfWork
) : ICommandHandler<AddImageCommand, AddImageCommandResponse>
{
    public async Task<Result<AddImageCommandResponse>> Handle(AddImageCommand command, CancellationToken cancellationToken)
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

        //TODO: debug why this throws error, expected one row to be added but 0 were added
        //salon.Images!.Add(newImage);

        await _images!.AddAsync(newImage);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new AddImageCommandResponse(newImage.Id);
    }
}
