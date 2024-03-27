using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Domain;
using SmartSalon.Application.Queries.Responses;
using SmartSalon.Application.ResultObject;

namespace SmartSalon.Application.Queries.Handlers;

public class GetByIdQueryHandler : IQueryHandler<GetByIdQuery, GetByIdQueryResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEfRepository<BookingTime> _repository;

    public GetByIdQueryHandler(IUnitOfWork unitOfWork, IEfRepository<BookingTime> repository)
    {
        _unitOfWork = unitOfWork;
        _repository = repository;
    }

    public async Task<Result<GetByIdQueryResponse>> Handle(GetByIdQuery query, CancellationToken cancellationToken)
    {
        var response = new GetByIdQueryResponse() { Name = "gosho", Age = 5 };

        return await Task.FromResult(response);
    }
}
