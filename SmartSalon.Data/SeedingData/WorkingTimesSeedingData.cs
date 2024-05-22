
using SmartSalon.Application.Domain.Salons;

namespace SmartSalon.Data.SeedingData;

internal static class WorkingTimesSeedingData
{
    public static WorkingTime[] Data = [
        new(){
            SalonId = SalonsSeedingData.FirstSalonId,

            MondayFrom = new (7, 0),
            MondayTo = new (19, 0),

            TuesdayFrom = new (7, 0),
            TuesdayTo = new (19, 0),

            WednesdayFrom = new (7, 0),
            WednesdayTo = new (19, 0),

            ThursdayFrom = new (7, 0),
            ThursdayTo = new (19, 0),

            FridayFrom = new (7, 0),
            FridayTo = new (19, 0),

            SaturdayFrom = new (7, 0),
            SaturdayTo = new (19, 0),

            SundayFrom = new (7, 0),
            SundayTo = new (19, 0)
        },
        new(){
            SalonId = SalonsSeedingData.SecondSalonId,

            MondayFrom = new (7, 0),
            MondayTo = new (19, 0),

            TuesdayFrom = new (7, 0),
            TuesdayTo = new (19, 0),

            WednesdayFrom = new (7, 0),
            WednesdayTo = new (19, 0),

            ThursdayFrom = new (7, 0),
            ThursdayTo = new (19, 0),

            FridayFrom = new (7, 0),
            FridayTo = new (19, 0),

            SaturdayFrom = new (7, 0),
            SaturdayTo = new (19, 0),

            SundayFrom = new (7, 0),
            SundayTo = new (19, 0)
        }
    ];
}

