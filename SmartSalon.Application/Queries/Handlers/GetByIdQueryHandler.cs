using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Domain;
using SmartSalon.Application.Queries.Responses;
using SmartSalon.Application.ResultObject;

namespace SmartSalon.Application.Queries.Handlers;

internal class GetByIdQueryHandler(IUnitOfWork _unitOfWork, IEfRepository<BookingTime> _repository) : IQueryHandler<GetByIdQuery, GetByIdQueryResponse>
{
    public async Task<Result<GetByIdQueryResponse>> Handle(GetByIdQuery query, CancellationToken cancellationToken)
    {
        var response = new GetByIdQueryResponse() { Name = "gosho", Age = 5 };

        return await Task.FromResult(response);
    }
}
