using AutoMapper;
using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Domain.Salons;
using SmartSalon.Application.Errors;
using SmartSalon.Application.ResultObject;

namespace SmartSalon.Application.Features.Salons.Queries;

public class GetSpecialtyByIdQuery(Id id) : IQuery<GetSpecialtyByIdQueryResponse>
{
    public Id SpecialtyId => id;
}

public class GetSpecialtyByIdQueryResponse : IMapFrom<Specialty>
{
    public required string Text { get; set; }
    public Id SalonId { get; set; }
    public virtual Salon? Salon { get; set; }
}

internal class GetSpecialtyByIdQueryHandler(IEfRepository<Specialty> _specialties, IMapper _mapper)
    : IQueryHandler<GetSpecialtyByIdQuery, GetSpecialtyByIdQueryResponse>
{
    public async Task<Result<GetSpecialtyByIdQueryResponse>> Handle(GetSpecialtyByIdQuery query, CancellationToken cancellationToken)
    {
        var specialty = await _specialties.GetByIdAsync(query.SpecialtyId);

        if (specialty is null)
        {
            return Error.NotFound;
        }

        return _mapper.Map<GetSpecialtyByIdQueryResponse>(specialty);
    }
}
