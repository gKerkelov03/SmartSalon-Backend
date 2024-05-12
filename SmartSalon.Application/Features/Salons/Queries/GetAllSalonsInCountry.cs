using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Domain.Salons;
using SmartSalon.Application.Errors;
using SmartSalon.Application.ResultObject;

namespace SmartSalon.Application.Features.Salons.Queries;

public class GetAllSalonsInCountryQuery(string country) : IQuery<IEnumerable<GetSalonByIdQueryResponse>>
{
    public string Country => country;
}
internal class GetAllSalonsInCountry(IEfRepository<Salon> _salons, IMapper _mapper)
    : IQueryHandler<GetAllSalonsInCountryQuery, IEnumerable<GetSalonByIdQueryResponse>>
{
    public async Task<Result<IEnumerable<GetSalonByIdQueryResponse>>> Handle(GetAllSalonsInCountryQuery query, CancellationToken cancellationToken)
    {
        var queryResponse = await _salons.All
            .Include(salon => salon.Workers)
            .Include(salon => salon.Owners)
            .Include(salon => salon.AcceptedCurrencies)
            .Include(salon => salon.Sections)
            .Include(salon => salon.Images)
            .Include(salon => salon.Specialties)
            .Where(salon => salon.Country == query.Country.ToUpper())
            .ProjectTo<GetSalonByIdQueryResponse>(_mapper.ConfigurationProvider)
            .ToListAsync();

        if (queryResponse is null)
        {
            return Error.NotFound;
        }

        return queryResponse;
    }
}
