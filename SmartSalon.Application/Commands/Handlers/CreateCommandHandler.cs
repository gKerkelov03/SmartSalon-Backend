using MediatR;
using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Commands.Responses;
using SmartSalon.Application.Domain;
using SmartSalon.Application.ResultObject;

namespace SmartSalon.Application.Commands.Handlers;

public class CreateCommandHandler : ICommandHandler<CreateCommand, CreateCommandResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEfRepository<BookingTime> _repository;

    public CreateCommandHandler(IUnitOfWork unitOfWork, IEfRepository<BookingTime> repository)
    {
        _unitOfWork = unitOfWork;
        _repository = repository;
    }

    public async Task<Result<CreateCommandResponse>> Handle(CreateCommand command, CancellationToken cancellationToken)
    {
        var response = new CreateCommandResponse(Id.NewGuid());
        var result = Result<CreateCommandResponse>.Success(response);
        return await Task.FromResult(result);
    }
}
