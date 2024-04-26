
using SmartSalon.Application.Domain.Salons;

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
            DefaultTimePenalty = 5,
            DefaultBookingsInAdvance = 5,
            SubscriptionsEnabled = true,
            WorkersCanMoveBookings = true,
            WorkersCanSetNonWorkingPeriods = true,
            WorkingTimeId = WorkingTimesSeedingData.Data[0].Id,
            MainCurrencyId = CurrenciesSeedingData.BulgarianLevId,
        },
        new()
        {
            Id = SecondSalonId,
            Name = "Gosho shop",
            Description = "Description",
            Location = "Location",
            DefaultTimePenalty = 5,
            DefaultBookingsInAdvance = 5,
            SubscriptionsEnabled = true,
            WorkersCanMoveBookings = true,
            WorkersCanSetNonWorkingPeriods = true,
            WorkingTimeId = WorkingTimesSeedingData.Data[1].Id,
            MainCurrencyId = CurrenciesSeedingData.BulgarianLevId,
        }
    ];
}

