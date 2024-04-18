
namespace SmartSalon.Application.Options;

public class HostingOptions
{
    public static string SectionName = "Hosting";
    public required string FrontendUrl { get; set; }
    public required string BackendUrl { get; set; }
}