using Microsoft.EntityFrameworkCore;
using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Abstractions.MediatR;
using SmartSalon.Application.Domain.Users;
using SmartSalon.Application.Errors;
using SmartSalon.Application.Extensions;
using SmartSalon.Application.ResultObject;

namespace SmartSalon.Application.Features.Users.Commands;

public class UpdateWorkerCommand : ICommand, IMapTo<Worker>
{
    public Id WorkerId { get; set; }
    public required string JobTitle { get; set; }
    public required string Nickname { get; set; }
}

internal class UpdateWorkerCommandHandler(IEfRepository<Worker> _workers, IUnitOfWork _unitOfWork)
    : ICommandHandler<UpdateWorkerCommand>
{
    public async Task<Result> Handle(UpdateWorkerCommand command, CancellationToken cancellationToken)
    {
        var worker = await _workers.GetByIdAsync(command.WorkerId);
        var salonId = worker!.SalonId;

        if (worker is null)
        {
            return Error.NotFound;
        }

        var workerWithTheSameNickname = await _workers.FirstOrDefaultAsync(worker =>
            worker.Nickname == command.Nickname &&
            worker.Id != command.WorkerId &&
            worker.SalonId == salonId
        );

        if (workerWithTheSameNickname is not null)
        {
            return Error.Conflict;
        }

        worker.MapAgainst(command);
        _workers.Update(worker);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
