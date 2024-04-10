namespace SmartSalon.Application.Options;

public class JwtOptions
{
    public const string SectionName = "Jwt";
    public required string EncryptionKey { get; set; }
    public required string Audience { get; set; }
    public required string Issuer { get; set; }
    public int TokenExpirationInDays => 30; //TODO: change to something more reasonable
}