using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Abstractions.MediatR;
using SmartSalon.Application.Domain.Salons;
using SmartSalon.Application.Errors;
using SmartSalon.Application.ResultObject;

namespace SmartSalon.Application.Features.Salons.Commands;

public class CreateImageCommand : ICommand<CreateImageCommandResponse>, IMapTo<Image>
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
    IUnitOfWork _unitOfWork,
    IMapper _mapper
) : ICommandHandler<CreateImageCommand, CreateImageCommandResponse>
{
    public async Task<Result<CreateImageCommandResponse>> Handle(CreateImageCommand command, CancellationToken cancellationToken)
    {
        var newImage = _mapper.Map<Image>(command);
        var salonDoesntExist = await _salons.GetByIdAsync(command.SalonId) is null;

        if (salonDoesntExist)
        {
            return Error.NotFound;
        }

        await _images!.AddAsync(newImage);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new CreateImageCommandResponse(newImage.Id);
    }
}
