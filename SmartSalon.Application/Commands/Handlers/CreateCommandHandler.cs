using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Commands.Responses;
using SmartSalon.Application.Domain;
using SmartSalon.Application.ResultObject;

namespace SmartSalon.Application.Commands.Handlers;

internal class CreateCommandHandler(IUnitOfWork _unitOfWork, IEfRepository<BookingTime> _repository)
    : ICommandHandler<CreateCommand, CreateCommandResponse>
{
    public async Task<Result<CreateCommandResponse>> Handle(CreateCommand command, CancellationToken cancellationToken)
    {
        var response = new CreateCommandResponse(Id.NewGuid());
        var result = Result<CreateCommandResponse>.Success(response);

        return await Task.FromResult(result);
    }
}
