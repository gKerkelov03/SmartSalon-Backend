using Microsoft.EntityFrameworkCore;
using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Abstractions.MediatR;
using SmartSalon.Application.Domain.Salons;
using SmartSalon.Application.Errors;
using SmartSalon.Application.ResultObject;

namespace SmartSalon.Application.Features.Salons.Commands;

public class CreateImageCommand : ICommand<CreateImageCommandResponse>
{
    public required string Url { get; set; }
    public Id SalonId { get; set; }
}

public class CreateImageCommandResponse(Id id)
{
    public Id CreatedImageId => id;
}

internal class CreateImageCommandHandler(
    IEfRepository<Salon> _salons,
    IEfRepository<Image> _images,
    IUnitOfWork _unitOfWork
) : ICommandHandler<CreateImageCommand, CreateImageCommandResponse>
{
    public async Task<Result<CreateImageCommandResponse>> Handle(CreateImageCommand command, CancellationToken cancellationToken)
    {
        var salon = await _salons.All
            .Include(salon => salon.Images)
            .FirstOrDefaultAsync(salon => salon.Id == command.SalonId);

        var newImage = new Image { SalonId = command.SalonId, Url = command.Url };

        if (salon is null)
        {
            return Error.NotFound;
        }

        //TODO: debug why this throws error, expected one row to be added but 0 were added
        //salon.Images!.Create(newImage);

        await _images!.AddAsync(newImage);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new CreateImageCommandResponse(newImage.Id);
    }
}
