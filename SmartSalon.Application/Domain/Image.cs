using SmartSalon.Application.Domain.Base;

namespace SmartSalon.Application.Domain;

public class Image : BaseEntity
{
    public string Url { get; set; }
    public Image(string url) => Url = url;
}