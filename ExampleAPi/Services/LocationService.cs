// 2) Services/LocationService.cs
// Keep your method signature exactly as you already have it.
// Only ADD more fallback fields so rural areas don't return null.

using System.Text.Json;

namespace ExampleAPi.Services;

public class LocationService : ILocationService
{
    private readonly HttpClient _httpClient;

    public LocationService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("ExampleAPI/1.0");
    }

    public async Task<string?> GetCityAsync(double longitude, double latitude)
    {
        var url =
            $"https://nominatim.openstreetmap.org/reverse?format=json&addressdetails=1&lat={latitude}&lon={longitude}&zoom=18";

        var response = await _httpClient.GetAsync(url);
        if (!response.IsSuccessStatusCode)
            return null;

        var json = await response.Content.ReadAsStringAsync();
        using var doc = JsonDocument.Parse(json);

        if (!doc.RootElement.TryGetProperty("address", out var address))
            return null;

        string? place = null;

        // Common urban keys
        if (address.TryGetProperty("city", out var cityProp)) place = cityProp.GetString();
        else if (address.TryGetProperty("town", out var townProp)) place = townProp.GetString();
        else if (address.TryGetProperty("village", out var villageProp)) place = villageProp.GetString();

        // Common rural keys (often why you get null)
        else if (address.TryGetProperty("hamlet", out var hamletProp)) place = hamletProp.GetString();
        else if (address.TryGetProperty("suburb", out var suburbProp)) place = suburbProp.GetString();
        else if (address.TryGetProperty("municipality", out var muniProp)) place = muniProp.GetString();
        else if (address.TryGetProperty("county", out var countyProp)) place = countyProp.GetString();

        // Last resort
        else if (address.TryGetProperty("state", out var stateProp)) place = stateProp.GetString();

        return string.IsNullOrWhiteSpace(place) ? null : place;
    }
}