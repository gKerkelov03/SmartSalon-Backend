namespace SmartSalon.Presentation.Web;

public class UnauthorizedResponse
{
    public required int ContentLength { get; set; }

    public required DateTimeOffset Date { get; set; }

    public required string Server { get; set; }

    public required string wwwAuthenticate { get; set; }
}
