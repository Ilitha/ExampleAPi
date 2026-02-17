using Microsoft.AspNetCore.Mvc.RazorPages;
using ExampleAPi.Services;
using System.Globalization;
using System.Text.Json;

namespace ExampleAPi.Pages;

public class IndexModel : PageModel
{
    private readonly IWeatherService _weatherService;
    private readonly ILocationService _locationService;
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(IWeatherService weatherService, ILogger<IndexModel> logger, ILocationService locationService)
    {
        _weatherService = weatherService;
        _logger = logger;
        _locationService = locationService;
    }

    public IEnumerable<WeatherForecast> WeatherForecasts { get; set; } = Enumerable.Empty<WeatherForecast>();
    public double CurrentLatitude { get; set; }
    public double CurrentLongitude { get; set; }
    public string? CityName { get; set; }

    // Added for the new UI controls
    public bool UseFahrenheit { get; set; }
    public string? LocationQuery { get; set; }

    public async Task OnGetAsync(double? latitude, double? longitude, string? unit, string? query)
    {
        UseFahrenheit = string.Equals(unit, "F", StringComparison.OrdinalIgnoreCase);
        LocationQuery = query;

        _logger.LogInformation(
            "OnGetAsync called with latitude: {Latitude}, longitude: {Longitude}, unit: {Unit}, query: {Query}",
            latitude, longitude, unit, query);

        // If user typed a place and didn't provide coords (or hidden fields are empty), try Open-Meteo geocoding.
        if (!string.IsNullOrWhiteSpace(query) && (!latitude.HasValue || !longitude.HasValue))
        {
            try
            {
                var coords = await TryOpenMeteoForwardGeocodeAsync(query);

                if (coords.HasValue)
                {
                    latitude = coords.Value.latitude;
                    longitude = coords.Value.longitude;
                    _logger.LogInformation("Open-Meteo geocode '{Query}' -> {Lat},{Lon}", query, latitude, longitude);
                }
                else
                {
                    _logger.LogWarning("Open-Meteo geocode returned no results for '{Query}'", query);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Open-Meteo forward geocode failed for '{Query}'", query);
            }
        }

        if (latitude.HasValue && longitude.HasValue)
        {
            // Guard: fix swapped values if they come in wrong
            if (latitude.Value is < -90 or > 90 && longitude.Value is >= -90 and <= 90)
            {
                _logger.LogWarning("Latitude/Longitude appear swapped. Swapping. lat={Lat}, lon={Lon}", latitude, longitude);
                (latitude, longitude) = (longitude, latitude);
            }

            CurrentLatitude = latitude.Value;
            CurrentLongitude = longitude.Value;

            try
            {
                _logger.LogInformation("Fetching weather for {Latitude}, {Longitude}", CurrentLatitude, CurrentLongitude);
                WeatherForecasts = await _weatherService.GetForecastAsync(CurrentLatitude, CurrentLongitude);
                _logger.LogInformation("Weather forecast loaded: {Count} items", WeatherForecasts.Count());

                // IMPORTANT: your LocationService signature is (longitude, latitude)
                CityName = await _locationService.GetCityAsync(CurrentLongitude, CurrentLatitude);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to fetch weather forecast for {Latitude}, {Longitude}", CurrentLatitude, CurrentLongitude);
                WeatherForecasts = Enumerable.Empty<WeatherForecast>();
            }
        }
        else
        {
            _logger.LogInformation("No location parameters provided, skipping weather fetch");
        }
    }

    private static async Task<(double latitude, double longitude)?> TryOpenMeteoForwardGeocodeAsync(string query)
    {
        using var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("ExampleAPI/1.0");

        var url = $"https://geocoding-api.open-meteo.com/v1/search?name={Uri.EscapeDataString(query.Trim())}&count=1&language=en&format=json";
        using var response = await httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
            return null;

        var json = await response.Content.ReadAsStringAsync();
        using var doc = JsonDocument.Parse(json);

        if (!doc.RootElement.TryGetProperty("results", out var results) ||
            results.ValueKind != JsonValueKind.Array ||
            results.GetArrayLength() == 0)
        {
            return null;
        }

        var first = results[0];

        if (!first.TryGetProperty("latitude", out var latProp) ||
            !first.TryGetProperty("longitude", out var lonProp))
        {
            return null;
        }

        var lat = latProp.GetDouble();
        var lon = lonProp.GetDouble();

        if (lat is < -90 or > 90) return null;
        if (lon is < -180 or > 180) return null;

        return (lat, lon);
    }
}