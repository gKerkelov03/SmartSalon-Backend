namespace SmartSalon.Presentation.Web.Users.Requests;

public class UpdateWorkerJobTitlesRequest
{
    public required Id WorkerId { get; set; }

    public required IEnumerable<Id> JobTitlesIds { get; set; }
}