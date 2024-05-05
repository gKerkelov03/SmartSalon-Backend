
using SmartSalon.Application.Domain.Salons;
using static SmartSalon.Data.SeedingData.CurrenciesSeedingData;

namespace SmartSalon.Data.SeedingData;

internal class SalonsSeedingData
{
    public static Id FirstSalonId = Id.NewGuid();
    public static Id SecondSalonId = Id.NewGuid();

    public static IEnumerable<Salon> Data = [
        new()
        {
            Id = FirstSalonId,
            Name = "Cosa Nostra",
            Description = "Description",
            Location = "Location",
            TimePenalty = 5,
            BookingsInAdvance = 5,
            SubscriptionsEnabled = true,
            WorkersCanMoveBookings = true,
            WorkersCanSetNonWorkingPeriods = true,
            WorkingTimeId = WorkingTimesSeedingData.Data[0].Id,
            MainCurrencyId = BulgarianLevId,
        },
        new()
        {
            Id = SecondSalonId,
            Name = "Gosho shop",
            Description = "Description",
            Location = "Location",
            TimePenalty = 5,
            BookingsInAdvance = 5,
            SubscriptionsEnabled = true,
            WorkersCanMoveBookings = true,
            WorkersCanSetNonWorkingPeriods = true,
            WorkingTimeId = WorkingTimesSeedingData.Data[1].Id,
            MainCurrencyId = BulgarianLevId,
        }
    ];
}

