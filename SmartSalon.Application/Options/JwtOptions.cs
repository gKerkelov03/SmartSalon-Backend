namespace SmartSalon.Application.Options;

public class JwtOptions
{
    public static string SectionName = "Jwt";

    public required string SecretKey { get; set; }

    public required string Audience { get; set; }

    public required string Issuer { get; set; }

    public int TokenExpirationInDays => 30;
}