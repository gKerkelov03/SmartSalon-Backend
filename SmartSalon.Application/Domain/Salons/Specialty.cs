using SmartSalon.Application.Domain.Base;

namespace SmartSalon.Application.Domain.Salons;

public class Specialty : DeletableEntity
{
    public required string Text { get; set; }
    public Id SalonId { get; set; }
    public virtual Salon? Salon { get; set; }
}

//Neck massage
//Play on a playstation
//free drinks
//free eyebrows fix
//shoe polish
//neck massage
//special beard olio
//music of choice
//virtual reality headset entertainment during the service
//Aromatherapy 
//hand massage
//warm towel
//Chance to win something
//Ticket for some event
//Phone chargers