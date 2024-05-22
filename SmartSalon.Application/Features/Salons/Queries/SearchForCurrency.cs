using AutoMapper;
using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Domain.Salons;
using SmartSalon.Application.Errors;
using SmartSalon.Application.Extensions;
using SmartSalon.Application.Features.Salons.Queries;
using SmartSalon.Application.ResultObject;

namespace SmartSalon.Application.Features.Users.Queries;

public class SearchForCurrencyQuery(string searchTerm) : IQuery<IEnumerable<GetCurrencyByIdQueryResponse>>
{
    public string SearchTerm => searchTerm;
}

internal class SearchForCurrencyQueryHandler(IEfRepository<Currency> _currencies, IMapper _mapper)
    : IQueryHandler<SearchForCurrencyQuery, IEnumerable<GetCurrencyByIdQueryResponse>>
{
    public async Task<Result<IEnumerable<GetCurrencyByIdQueryResponse>>> Handle(SearchForCurrencyQuery query, CancellationToken cancellationToken)
    {
        var searchTerm = query.SearchTerm.ToLower();

        var currenciesMatchingTheSearchTerm = await _currencies.FindAllAsync(currency =>
            currency.Name.ToLower().StartsWith(searchTerm) ||
            currency.Code.ToLower().StartsWith(searchTerm)
        );

        if (currenciesMatchingTheSearchTerm.IsEmpty())
        {
            return Error.NotFound;
        }

        return currenciesMatchingTheSearchTerm.ToListOf<GetCurrencyByIdQueryResponse>(_mapper);
    }
}
