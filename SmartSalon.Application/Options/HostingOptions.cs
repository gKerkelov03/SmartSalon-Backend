
namespace SmartSalon.Application.Options;

public class HostingOptions
{
    public static string SectionName = "Hostin";
    public required string FrontendUrl { get; set; }
    public required string BackendUrl { get; set; }
}