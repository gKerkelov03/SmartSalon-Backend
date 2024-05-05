using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Abstractions.MediatR;
using SmartSalon.Application.Domain.Users;
using SmartSalon.Application.Errors;
using SmartSalon.Application.Extensions;
using SmartSalon.Application.ResultObject;

namespace SmartSalon.Application.Features.Users.Commands;

public class UpdateWorkerCommand : ICommand
{
    public Id WorkerId { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string ProfilePictureUrl { get; set; }
    public required string PhoneNumber { get; set; }
    public required string Nickname { get; set; }
}

internal class UpdateWorkerCommandHandler(IEfRepository<Worker> _workers, IUnitOfWork _unitOfWork)
    : ICommandHandler<UpdateWorkerCommand>
{
    public async Task<Result> Handle(UpdateWorkerCommand command, CancellationToken cancellationToken)
    {
        var worker = await _workers.GetByIdAsync(command.WorkerId);

        if (worker is null)
        {
            return Error.NotFound;
        }

        worker.MapAgainst(command);
        _workers.Update(worker);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
