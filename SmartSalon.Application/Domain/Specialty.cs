using SmartSalon.Application.Domain.Base;

namespace SmartSalon.Application.Domain;

public class Specialty : BaseEntity
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
//nqkvo specialno maslo s koeto ti majat bradata primerno
//music of choice
//virtual reality headset entertainment during the service
//Aromatherapy - da palqt sveshti deto mirishat hubavo i imat uj uspokoqvashti lechebni efekti
//hand massage
//warm towel
//Chance to win something
//Ticket for some event
//Phone chargers