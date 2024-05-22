using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Abstractions.MediatR;
using SmartSalon.Application.Domain.Salons;
using SmartSalon.Application.Errors;
using SmartSalon.Application.Extensions;
using SmartSalon.Application.ResultObject;

namespace SmartSalon.Application.Features.Salons.Commands;

public class UpdateSpecialtyCommand : ICommand
{
    public required Id SpecialtyId { get; set; }
    public required string Text { get; set; }
}

public class UpdateSpecialtyCommandResponse : ICommand
{
    public required Id SalonId { get; set; }
    public required string Text { get; set; }
}

internal class UpdateSpecialtyCommandHandler(IEfRepository<Specialty> _specialties, IUnitOfWork _unitOfWork)
    : ICommandHandler<UpdateSpecialtyCommand>
{
    public async Task<Result> Handle(UpdateSpecialtyCommand command, CancellationToken cancellationToken)
    {
        var specalty = await _specialties.GetByIdAsync(command.SpecialtyId);

        if (specalty is null)
        {
            return Error.NotFound;
        }

        specalty.MapAgainst(command);
        _specialties.Update(specalty);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
