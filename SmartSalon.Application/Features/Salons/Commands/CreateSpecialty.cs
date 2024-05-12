using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Abstractions.MediatR;
using SmartSalon.Application.Domain.Salons;
using SmartSalon.Application.Errors;
using SmartSalon.Application.ResultObject;

namespace SmartSalon.Application.Features.Salons.Commands;

public class CreateSpecialtyCommand : ICommand<CreateSpecialtyCommandResponse>, IMapTo<Specialty>
{
    public required Id SalonId { get; set; }
    public required string Text { get; set; }
}

public class CreateSpecialtyCommandResponse(Id id)
{
    public Id CreatedSpecialtyId => id;
}

internal class CreateSpecialtyCommandHandler(
    IEfRepository<Salon> _salons,
    IEfRepository<Specialty> _specialties,
    IUnitOfWork _unitOfWork,
    IMapper _mapper
)
    : ICommandHandler<CreateSpecialtyCommand, CreateSpecialtyCommandResponse>
{
    public async Task<Result<CreateSpecialtyCommandResponse>> Handle(CreateSpecialtyCommand command, CancellationToken cancellationToken)
    {
        var newSpecialty = _mapper.Map<Specialty>(command);

        var salon = await _salons.All
            .Include(salon => salon.Specialties)
            .FirstOrDefaultAsync(salon => salon.Id == command.SalonId);

        if (salon is null)
        {
            return Error.NotFound;
        }

        var salonAlreadyContainsSpecialty = salon.Specialties!.Any(specialty => specialty.Text == newSpecialty.Text);

        if (salonAlreadyContainsSpecialty)
        {
            return Error.Conflict;
        }

        await _specialties.AddAsync(newSpecialty);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new CreateSpecialtyCommandResponse(newSpecialty.Id);
    }
}
