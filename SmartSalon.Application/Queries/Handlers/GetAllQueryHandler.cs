using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Domain;
using SmartSalon.Application.Queries.Responses;
using SmartSalon.Application.ResultObject;

namespace SmartSalon.Application.Queries.Handlers;

public class GetAllQueryHandler : IQueryHandler<GetAllQuery, IEnumerable<GetByIdQueryResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEfRepository<BookingTime> _repository;

    public GetAllQueryHandler(IUnitOfWork unitOfWork, IEfRepository<BookingTime> repository)
    {
        _unitOfWork = unitOfWork;
        _repository = repository;
    }

    public async Task<Result<IEnumerable<GetByIdQueryResponse>>> Handle(GetAllQuery query, CancellationToken cancellationToken)
    {
        var response = Enumerable.Repeat(new GetByIdQueryResponse() { Name = "gosho", Age = 5 }, 5);
        var result = Result<IEnumerable<GetByIdQueryResponse>>.Success(response);
        return await Task.FromResult(result);
    }
}
