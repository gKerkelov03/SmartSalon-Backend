using Microsoft.EntityFrameworkCore;
using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Abstractions.MediatR;
using SmartSalon.Application.Domain;
using SmartSalon.Application.Domain.Salons;
using SmartSalon.Application.Errors;
using SmartSalon.Application.ResultObject;

namespace SmartSalon.Application.Features.Salons.Commands;

public class RemoveCurrencyCommand : ICommand
{
    public Id CurrencyId { get; set; }
    public Id SalonId { get; set; }
}

internal class RemoveCurrencyCommandHandler(
    IEfRepository<Currency> _currencies,
    IEfRepository<Salon> _salons,
    IUnitOfWork _unitOfWork
)
    : ICommandHandler<RemoveCurrencyCommand>
{
    public async Task<Result> Handle(RemoveCurrencyCommand command, CancellationToken cancellationToken)
    {
        var currency = await _currencies.GetByIdAsync(command.CurrencyId);

        if (currency is null)
        {
            return Error.NotFound;
        }

        var salon = await _salons.All
            .Include(salon => salon.Currencies)
            .Where(salon => salon.Id == command.SalonId)
            .FirstOrDefaultAsync();

        if (salon is null)
        {
            return Error.NotFound;
        }

        salon.Currencies!.Remove(currency);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
