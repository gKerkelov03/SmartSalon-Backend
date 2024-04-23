using AutoMapper;
using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Domain;
using SmartSalon.Application.Errors;
using SmartSalon.Application.ResultObject;

namespace SmartSalon.Application.Features.Salons.Queries;

public class GetCurrencyByIdQuery(Id id) : IQuery<GetCurrencyByIdQueryResponse>
{
    public Id CurrencyId => id;
}

public class GetCurrencyByIdQueryResponse : IMapFrom<Currency>
{
    public required string Code { get; set; }
    public required string Name { get; set; }
    public required string Country { get; set; }
}

internal class GetCurrencyByIdQueryHandler(IEfRepository<Currency> _currencies, IMapper _mapper)
    : IQueryHandler<GetCurrencyByIdQuery, GetCurrencyByIdQueryResponse>
{
    public async Task<Result<GetCurrencyByIdQueryResponse>> Handle(GetCurrencyByIdQuery query, CancellationToken cancellationToken)
    {
        var currency = await _currencies.GetByIdAsync(query.CurrencyId);

        if (currency is null)
        {
            return Error.NotFound;
        }

        return _mapper.Map<GetCurrencyByIdQueryResponse>(currency);
    }
}