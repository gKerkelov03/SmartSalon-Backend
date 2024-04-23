using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Abstractions.MediatR;
using SmartSalon.Application.Domain.Salons;
using SmartSalon.Application.Errors;
using SmartSalon.Application.ResultObject;

namespace SmartSalon.Application.Features.Salons.Commands;

public class AddSpecialtyCommand : ICommand<AddSpecialtyCommandResponse>, IMapTo<Specialty>
{
    public required Id SalonId { get; set; }
    public required string Text { get; set; }
}

public class AddSpecialtyCommandResponse(Id id)
{
    public Id CreatedSpecialtyId => id;
}

internal class AddSpecialtyCommandHandler(IEfRepository<Salon> _salons, IUnitOfWork _unitOfWork, IMapper _mapper)
    : ICommandHandler<AddSpecialtyCommand, AddSpecialtyCommandResponse>
{
    public async Task<Result<AddSpecialtyCommandResponse>> Handle(AddSpecialtyCommand command, CancellationToken cancellationToken)
    {
        var specialty = _mapper.Map<Specialty>(command);

        var salon = await _salons.All
            .Include(salon => salon.Currencies)
            .Where(salon => salon.Id == command.SalonId)
            .FirstOrDefaultAsync();

        if (salon is null)
        {
            return Error.NotFound;
        }

        var salonAlreadyContainsSpecialty = salon.Specialties!.Any(specialty => specialty.Text == command.Text);

        if (salonAlreadyContainsSpecialty)
        {
            return Error.Conflict;
        }

        salon.Specialties!.Add(specialty);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new AddSpecialtyCommandResponse(specialty.Id);
    }
}
