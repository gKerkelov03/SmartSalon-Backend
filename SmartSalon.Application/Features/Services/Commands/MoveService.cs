
using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Abstractions.MediatR;
using SmartSalon.Application.Domain.Services;
using SmartSalon.Application.Errors;
using SmartSalon.Application.Extensions;
using SmartSalon.Application.ResultObject;

namespace SmartService.Application.Features.Services.Commands;

public class MoveServiceCommand : ICommand
{
    public required Id ServiceId { get; set; }
    public Id CategoryId { get; set; }
    public required int Order { get; set; }
}

internal class MoveServiceCommandHandler(IEfRepository<Service> _services, IUnitOfWork _unitOfWork)
    : ICommandHandler<MoveServiceCommand>
{
    public async Task<Result> Handle(MoveServiceCommand command, CancellationToken cancellationToken)
    {
        var service = await _services.GetByIdAsync(command.ServiceId);

        if (service is null)
        {
            return Error.NotFound;
        }

        service.MapAgainst(command);
        _services.Update(service);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
