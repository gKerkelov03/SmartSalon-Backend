using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Abstractions.MediatR;
using SmartSalon.Application.Domain.Salons;
using SmartSalon.Application.Errors;
using SmartSalon.Application.Extensions;
using SmartSalon.Application.ResultObject;

namespace SmartSalon.Application.Features.Salons.Commands;

public class RemoveImageCommand : ICommand, IMapTo<Salon>
{
    public Id ImageId { get; set; }
    public Id SalonId { get; set; }
}

internal class RemoveImageCommandHandler(IEfRepository<Salon> users, IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateSalonCommand>
{
    public async Task<Result> Handle(UpdateSalonCommand command, CancellationToken cancellationToken)
    {
        var user = await users.GetByIdAsync(command.SalonId);

        if (user is null)
        {
            return Error.NotFound;
        }

        user.MapAgainst(command);
        users.Update(user);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
