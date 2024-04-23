using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Abstractions.MediatR;
using SmartSalon.Application.Domain.Salons;
using SmartSalon.Application.Errors;
using SmartSalon.Application.ResultObject;

namespace SmartSalon.Application.Features.Salons.Commands;

public class DeleteSalonCommand(Id id) : ICommand
{
    public Id SalonId => id;
}

internal class DeleteSalonCommandHandler(IEfRepository<Salon> _salons, IUnitOfWork _unitOfWork)
    : ICommandHandler<DeleteSalonCommand>
{
    public async Task<Result> Handle(DeleteSalonCommand command, CancellationToken cancellationToken)
    {
        var salon = await _salons.GetByIdAsync(command.SalonId);

        if (salon is null)
        {
            return Error.NotFound;
        }

        await _salons.RemoveByIdAsync(salon.Id);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return await Task.FromResult(Result.Success());
    }
}
