using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Abstractions.MediatR;
using SmartSalon.Application.Domain.Services;
using SmartSalon.Application.Errors;
using SmartSalon.Application.ResultObject;

namespace SmartSalon.Application.Features.Services.Commands;

public class DeleteServiceCommand : ICommand
{
    public Id ServiceId { get; set; }
}

internal class DeleteServiceCommandHandler(IEfRepository<Service> _services, IUnitOfWork _unitOfWork)
    : ICommandHandler<DeleteServiceCommand>
{
    public async Task<Result> Handle(DeleteServiceCommand command, CancellationToken cancellationToken)
    {
        var service = await _services.GetByIdAsync(command.ServiceId);

        if (service is null)
        {
            return Error.NotFound;
        }

        await _services.RemoveByIdAsync(service.Id);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
