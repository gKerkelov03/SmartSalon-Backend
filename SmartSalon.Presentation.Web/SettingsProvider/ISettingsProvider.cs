
namespace SmartSalon.Presentation.Web.Settings;

public interface ISettingsProvider
{
    public string JwtSecret { get; set; }

    public string ConnectionString { get; set; }
}