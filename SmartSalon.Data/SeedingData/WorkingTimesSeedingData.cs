
using SmartSalon.Application.Domain.Salons;

namespace SmartSalon.Data.SeedingData;

internal static class WorkingTimesSeedingData
{
    public static WorkingTime[] Data = [
        new(){
            SalonId = SalonsSeedingData.FirstSalonId,

            MondayOpeningTime = new (7, 0),
            MondayClosingTime = new (19, 0),

            TuesdayOpeningTime = new (7, 0),
            TuesdayClosingTime = new (19, 0),

            WednesdayOpeningTime = new (7, 0),
            WednesdayClosingTime = new (19, 0),

            ThursdayOpeningTime = new (7, 0),
            ThursdayClosingTime = new (19, 0),

            FridayOpeningTime = new (7, 0),
            FridayClosingTime = new (19, 0),

            SaturdayOpeningTime = new (7, 0),
            SaturdayClosingTime = new (19, 0),

            SundayOpeningTime = new (7, 0),
            SundayClosingTime = new (19, 0)
        },
        new(){
            SalonId = SalonsSeedingData.SecondSalonId,

            MondayOpeningTime = new (7, 0),
            MondayClosingTime = new (19, 0),

            TuesdayOpeningTime = new (7, 0),
            TuesdayClosingTime = new (19, 0),

            WednesdayOpeningTime = new (7, 0),
            WednesdayClosingTime = new (19, 0),

            ThursdayOpeningTime = new (7, 0),
            ThursdayClosingTime = new (19, 0),

            FridayOpeningTime = new (7, 0),
            FridayClosingTime = new (19, 0),

            SaturdayOpeningTime = new (7, 0),
            SaturdayClosingTime = new (19, 0),

            SundayOpeningTime = new (7, 0),
            SundayClosingTime = new (19, 0)
        }
    ];
}

