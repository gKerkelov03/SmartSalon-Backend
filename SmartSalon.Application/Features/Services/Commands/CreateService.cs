using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Abstractions.MediatR;
using SmartSalon.Application.Domain.Salons;
using SmartSalon.Application.Domain.Services;
using SmartSalon.Application.Errors;
using SmartSalon.Application.ResultObject;

namespace SmartSalon.Application.Features.Services.Commands;

public class CreateServiceCommand : ICommand, IMapTo<Service>
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required double Price { get; set; }
    public required int DurationInMinutes { get; set; }
    public Id SalonId { get; set; }
    public Id CategoryId { get; set; }
}

internal class CreateServiceCommandHandler(
    IEfRepository<Salon> _salons,
    IUnitOfWork _unitOfWork,
    IMapper _mapper
) : ICommandHandler<CreateServiceCommand>
{
    public async Task<Result> Handle(CreateServiceCommand command, CancellationToken cancellationToken)
    {
        //TODO: set order last
        var newService = _mapper.Map<Service>(command);

        var salon = await _salons.All
            .Include(salon => salon.Services)
            .Where(salon => salon.Id == command.SalonId)
            .FirstOrDefaultAsync();

        if (salon is null)
        {
            return Error.NotFound;
        }

        var salonAlreadyContainsService = salon.Services!.Any(service => service.Name == command.Name);

        if (salonAlreadyContainsService)
        {
            return Error.Conflict;
        }

        salon.Services!.Add(newService);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
