using Microsoft.EntityFrameworkCore;
using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Domain.Salons;
using SmartSalon.Application.Errors;
using SmartSalon.Application.ResultObject;

namespace SmartSalon.Application.Features.Salons.Queries;

public class GetAllSalonsQuery : IQuery<IEnumerable<GetSalonByIdQueryResponse>>
{
}

internal class GetAllSalons(IEfRepository<Salon> _salons)
    : IQueryHandler<GetAllSalonsQuery, IEnumerable<GetSalonByIdQueryResponse>>
{
    public async Task<Result<IEnumerable<GetSalonByIdQueryResponse>>> Handle(GetAllSalonsQuery query, CancellationToken cancellationToken)
    {
        var queryResponse = await _salons.All
            .Include(salon => salon.Workers)
            .Include(salon => salon.Owners)
            .Include(salon => salon.AcceptedCurrencies)
            .Include(salon => salon.Categories)
            .Include(salon => salon.Sections)
            .Include(salon => salon.Services)
            .Include(salon => salon.Images)
            .Include(salon => salon.Specialties)
            .Select(salon => new GetSalonByIdQueryResponse
            {
                Id = salon.Id,
                Name = salon.Name,
                Description = salon.Description,
                Location = salon.Location,
                ProfilePictureUrl = salon.ProfilePictureUrl,
                TimePenalty = salon.TimePenalty,
                BookingsInAdvance = salon.BookingsInAdvance,
                SubscriptionsEnabled = salon.SubscriptionsEnabled,
                WorkersCanMoveBookings = salon.WorkersCanMoveBookings,
                WorkersCanSetNonWorkingPeriods = salon.WorkersCanSetNonWorkingPeriods,
                WorkingTimeId = salon.WorkingTimeId,
                MainCurrencyId = salon.MainCurrencyId,
                Currencies = salon.AcceptedCurrencies!.Select(currency => currency.Id),
                Owners = salon.Owners!.Select(owners => owners.Id),
                Workers = salon.Workers!.Select(workers => workers.Id),
                Specialties = salon.Specialties!.Select(specialty => specialty.Id),
                Sections = salon.Sections!.Select(section => section.Id),
                Categories = salon.Categories!.Select(category => category.Id),
                Services = salon.Services!.Select(service => service.Id),
                Images = salon.Images!.Select(image => image.Id)
            }).ToListAsync();

        if (queryResponse is null)
        {
            return Error.NotFound;
        }

        return queryResponse;
    }
}
