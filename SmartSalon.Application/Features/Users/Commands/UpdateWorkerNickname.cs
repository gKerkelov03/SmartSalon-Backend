using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Abstractions.MediatR;
using SmartSalon.Application.Domain.Users;
using SmartSalon.Application.Errors;
using SmartSalon.Application.ResultObject;

namespace SmartSalon.Application.Features.Users.Commands;

public class UpdateWorkerNicknameCommand : ICommand
{
    public Id WorkerId { get; set; }
    public required string Nickname { get; set; }
}

internal class UpdateWorkerNicknameCommandHandler(IEfRepository<Worker> _workers, IUnitOfWork _unitOfWork)
    : ICommandHandler<UpdateWorkerNicknameCommand>
{
    public async Task<Result> Handle(UpdateWorkerNicknameCommand command, CancellationToken cancellationToken)
    {
        var worker = await _workers.GetByIdAsync(command.WorkerId);

        if (worker is null)
        {
            return Error.NotFound;
        }

        worker.Nickname = command.Nickname;
        _workers.Update(worker);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
