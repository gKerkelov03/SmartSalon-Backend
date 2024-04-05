using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Abstractions.MediatR;
using SmartSalon.Application.Domain.Users;
using SmartSalon.Application.Errors;
using SmartSalon.Application.Extensions;
using SmartSalon.Application.ResultObject;

namespace SmartSalon.Application.Features.Workers.Commands;

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
        var workerWithTheSameNickname = await _workers.FirstAsync(worker =>
            worker.Nickname == command.Nickname &&
            worker.Id != command.WorkerId
        );

        if (workerWithTheSameNickname is not null)
        {
            return Error.Conflict;
        }

        var worker = await _workers.GetByIdAsync(command.WorkerId);

        if (worker is null)
        {
            return Error.NotFound;
        }

        worker.MapAgainst(command);
        _workers.Update(worker);

        await _unitOfWork.SaveAsync(cancellationToken);

        return Result.Success();
    }
}
