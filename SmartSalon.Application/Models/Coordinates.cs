namespace SmartSalon.Application.Models;

public class Coordinates(string country, string lat, string lng)
{
    public string Country => country;
    public string Latitude => lat;
    public string Longitude => lng;
}