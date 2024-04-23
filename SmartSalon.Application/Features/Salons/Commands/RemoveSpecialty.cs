using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Abstractions.MediatR;
using SmartSalon.Application.Domain.Salons;
using SmartSalon.Application.Errors;
using SmartSalon.Application.ResultObject;

namespace SmartSalon.Application.Features.Salons.Commands;

public class DeleteSpecialtyCommand(Id specialtyId) : ICommand
{
    public Id SpecialtyId => specialtyId;
}

internal class DeleteSpecialtyCommandHandler(IEfRepository<Specialty> _specialties, IUnitOfWork _unitOfWork)
    : ICommandHandler<DeleteSpecialtyCommand>
{
    public async Task<Result> Handle(DeleteSpecialtyCommand command, CancellationToken cancellationToken)
    {
        var specialty = await _specialties.GetByIdAsync(command.SpecialtyId);

        if (specialty is null)
        {
            return Error.NotFound;
        }

        await _specialties.RemoveByIdAsync(specialty.Id);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
