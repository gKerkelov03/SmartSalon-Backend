using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Domain.Salons;
using SmartSalon.Application.Errors;
using SmartSalon.Application.ResultObject;

namespace SmartSalon.Application.Features.Salons.Queries;

public class GetAllSalonsQuery : IQuery<IEnumerable<GetSalonByIdQueryResponse>>
{
}

//TODO make pagable by city and country 
internal class GetAllSalons(IEfRepository<Salon> _salons, IMapper _mapper)
    : IQueryHandler<GetAllSalonsQuery, IEnumerable<GetSalonByIdQueryResponse>>
{
    public async Task<Result<IEnumerable<GetSalonByIdQueryResponse>>> Handle(GetAllSalonsQuery query, CancellationToken cancellationToken)
    {
        var queryResponse = await _salons.All
            .Include(salon => salon.Workers)
            .Include(salon => salon.Owners)
            .Include(salon => salon.AcceptedCurrencies)
            .Include(salon => salon.Sections)
            .Include(salon => salon.Images)
            .Include(salon => salon.Specialties)
            .ProjectTo<GetSalonByIdQueryResponse>(_mapper.ConfigurationProvider)
            .ToListAsync();

        if (queryResponse is null)
        {
            return Error.NotFound;
        }

        return queryResponse;
    }
}
