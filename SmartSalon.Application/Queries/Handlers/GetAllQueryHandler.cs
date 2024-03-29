using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Domain;
using SmartSalon.Application.Queries.Responses;
using SmartSalon.Application.ResultObject;

namespace SmartSalon.Application.Queries.Handlers;

internal class GetAllQueryHandler(IUnitOfWork _unitOfWork, IEfRepository<BookingTime> _repository)
    : IQueryHandler<GetAllQuery, IEnumerable<GetByIdQueryResponse>>
{
    public async Task<Result<IEnumerable<GetByIdQueryResponse>>> Handle(GetAllQuery query, CancellationToken cancellationToken)
    {
        var response = Enumerable.Repeat(new GetByIdQueryResponse() { Name = "gosho", Age = 5 }, 5);
        var result = Result<IEnumerable<GetByIdQueryResponse>>.Success(response);

        return await Task.FromResult(result);
    }
}
