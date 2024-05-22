
using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Abstractions.MediatR;
using SmartSalon.Application.Domain.Services;
using SmartSalon.Application.Errors;
using SmartSalon.Application.Extensions;
using SmartSalon.Application.ResultObject;

namespace SmartService.Application.Features.Services.Commands;

public class UpdateServiceCommand : ICommand
{
    public required Id ServiceId { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required double Price { get; set; }
    public required int DurationInMinutes { get; set; }
}

internal class UpdateServiceCommandHandler(IEfRepository<Service> _services, IUnitOfWork _unitOfWork)
    : ICommandHandler<UpdateServiceCommand>
{
    public async Task<Result> Handle(UpdateServiceCommand command, CancellationToken cancellationToken)
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
