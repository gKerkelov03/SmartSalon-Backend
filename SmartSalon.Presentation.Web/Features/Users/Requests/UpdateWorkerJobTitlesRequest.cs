using SmartSalon.Presentation.Web.Attributes;

namespace SmartSalon.Presentation.Web.Users.Requests;

public class UpdateWorkerJobTitlesRequest
{
    [ComesFromRoute(IdRoute)]
    public required Id WorkerId { get; set; }
    public required Id SalonId { get; set; }
    public required IEnumerable<Id> JobTitlesIds { get; set; }
}