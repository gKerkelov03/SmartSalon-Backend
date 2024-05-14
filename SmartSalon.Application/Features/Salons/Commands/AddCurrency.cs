using Microsoft.EntityFrameworkCore;
using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Abstractions.MediatR;
using SmartSalon.Application.Domain.Salons;
using SmartSalon.Application.Errors;
using SmartSalon.Application.ResultObject;

namespace SmartSalon.Application.Features.Salons.Commands;

public class AddCurrencyCommand : ICommand
{
    public Id CurrencyId { get; set; }
    public Id SalonId { get; set; }
}

internal class AddCurrencyCommandHandler(IEfRepository<Currency> _currencies, IEfRepository<Salon> _salons, IUnitOfWork _unitOfWork)
    : ICommandHandler<AddCurrencyCommand>
{
    public async Task<Result> Handle(AddCurrencyCommand command, CancellationToken cancellationToken)
    {
        var currency = await _currencies.GetByIdAsync(command.CurrencyId);

        if (currency is null)
        {
            return Error.NotFound;
        }

        var salon = await _salons.All
            .Include(salon => salon.AcceptedCurrencies)
            .FirstOrDefaultAsync(salon => salon.Id == command.SalonId);

        if (salon is null)
        {
            return Error.NotFound;
        }

        if (salon.MainCurrencyId == command.SalonId)
        {
            return Error.Conflict;
        }

        var salonAlreadyContainsCurrency = salon.AcceptedCurrencies!.Any(currency => currency.Id == command.CurrencyId);

        if (salonAlreadyContainsCurrency)
        {
            return Error.Conflict;
        }

        salon.AcceptedCurrencies!.Add(currency);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
