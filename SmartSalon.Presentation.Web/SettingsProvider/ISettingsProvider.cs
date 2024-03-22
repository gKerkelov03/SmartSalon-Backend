
namespace SmartSalon.Presentation.Web.Settings;
//TODO: write this using Configurations and Options
public interface ISettingsProvider
{
    public string JwtSecret { get; set; }

    public string ConnectionString { get; set; }
}