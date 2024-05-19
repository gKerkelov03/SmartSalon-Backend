using System.Text.Json;
using Microsoft.Extensions.Options;
using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Errors;
using SmartSalon.Application.Models;
using SmartSalon.Application.Options;
using SmartSalon.Application.ResultObject;

namespace SmartSalon.Application.Services;

public class Geolocator(IOptions<ApiKeyOptions> _apikeyOptions, IHttpClientFactory _httpClientFactory) : IGeolocator
{
    private readonly string geolocationApiUrl =
        $"https://maps.googleapis.com/maps/api/geocode/json?key={_apikeyOptions.Value.GoogleMaps}";

    public async Task<Result<Coordinates>> GetCoordinatesAsync(string address)
    {
        var httpClient = _httpClientFactory.CreateClient();
        var url = $"{geolocationApiUrl}&address={Uri.EscapeDataString(address)}";
        var invalidAddressError = new Error("Invalid Google Maps address");

        var response = await httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
        {
            return invalidAddressError;
        }

        var responseBody = await response.Content.ReadAsStringAsync();
        var json = JsonDocument.Parse(responseBody);
        var results = json.RootElement.GetProperty("results");

        if (results.GetArrayLength() == 0)
        {
            return invalidAddressError;
        }

        var addressComponents = results[0].GetProperty("address_components");

        string country = "";
        foreach (var component in addressComponents.EnumerateArray())
        {
            bool shouldBreak = false;
            var types = component.GetProperty("types");

            foreach (var type in types.EnumerateArray())
            {
                if (type.GetString() == "country")
                {
                    country = component.GetProperty("long_name").GetString()!;
                    shouldBreak = true;
                    break;
                }
            }

            if (shouldBreak)
            {
                break;
            }
        }

        if (string.IsNullOrEmpty(country))
        {
            return invalidAddressError;
        }

        var location = results[0].GetProperty("geometry").GetProperty("location");

        var lat = location.GetProperty("lat").GetDecimal().ToString();
        var lng = location.GetProperty("lng").GetDecimal().ToString();

        return new Coordinates(country, lat, lng);
    }
}