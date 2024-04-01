using MediatR;
using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Abstractions.MediatR;
using SmartSalon.Application.Domain;
using SmartSalon.Application.ResultObject;

namespace SmartSalon.Application.Features.Users.Commands;

public class CreateOwnerCommand : ICommand<CreateOwnerCommandResponse>
{
    public required Id SalonId { get; set; }

    public required string UserName { get; set; }

    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    public required string Email { get; set; }

    public required string Password { get; set; }
}

public class CreateOwnerCommandResponse
{
    public Id CreatedOwnerId { get; set; }
}

internal class CreateOwnerCommandHandler(IUnitOfWork _unitOfWork, IEfRepository<Owner> _repository, IPublisher _publisher)
    : ICommandHandler<CreateOwnerCommand, CreateOwnerCommandResponse>
{
    public async Task<Result<CreateOwnerCommandResponse>> Handle(CreateOwnerCommand command, CancellationToken cancellationToken)
    {
        return await Task.FromResult(new CreateOwnerCommandResponse() { });
    }
}
