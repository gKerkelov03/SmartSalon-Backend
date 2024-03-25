
namespace SmartSalon.Presentation.Web.Models.Responses;

public class ProblemDetailsWithErrors
{
    public required string Title { get; set; }

    public required string Type { get; set; }

    public int Status { get; set; }

    public required IEnumerable<object> Errors { get; set; }

}

