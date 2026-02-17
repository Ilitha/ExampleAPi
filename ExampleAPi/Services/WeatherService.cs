using System.Text.Json;

namespace ExampleAPi.Services;

public class WeatherService : IWeatherService
{
    private readonly HttpClient _httpClient;

    public WeatherService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<WeatherForecast>> GetForecastAsync(double latitude, double longitude)
    {
        var url = $"https://api.open-meteo.com/v1/forecast?latitude={latitude}&longitude={longitude}&daily=temperature_2m_max,temperature_2m_min,weathercode&timezone=auto&forecast_days=5";

        var response = await _httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        var weatherData = JsonSerializer.Deserialize<OpenMeteoResponse>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        if (weatherData?.Daily == null)
        {
            return Enumerable.Empty<WeatherForecast>();
        }

        var forecasts = new List<WeatherForecast>();
        for (int i = 0; i < weatherData.Daily.Time.Length; i++)
        {
            forecasts.Add(new WeatherForecast
            {
                Date = DateOnly.Parse(weatherData.Daily.Time[i]),
                TemperatureC = (int)Math.Round((weatherData.Daily.Temperature_2m_Max[i] + weatherData.Daily.Temperature_2m_Min[i]) / 2),
                Summary = GetWeatherSummary(weatherData.Daily.Weathercode[i])
            });
        }

        return forecasts;
    }

    private string GetWeatherSummary(int weatherCode)
    {
        return weatherCode switch
        {
            0 => "Clear",
            1 or 2 or 3 => "Partly Cloudy",
            45 or 48 => "Foggy",
            51 or 53 or 55 => "Drizzle",
            61 or 63 or 65 => "Rainy",
            71 or 73 or 75 => "Snowy",
            77 => "Snow Grains",
            80 or 81 or 82 => "Rain Showers",
            85 or 86 => "Snow Showers",
            95 => "Thunderstorm",
            96 or 99 => "Thunderstorm with Hail",
            _ => "Unknown"
        };
    }

    private class OpenMeteoResponse
    {
        public DailyData? Daily { get; set; }
    }

    private class DailyData
    {
        public string[] Time { get; set; } = Array.Empty<string>();
        public double[] Temperature_2m_Max { get; set; } = Array.Empty<double>();
        public double[] Temperature_2m_Min { get; set; } = Array.Empty<double>();
        public int[] Weathercode { get; set; } = Array.Empty<int>();
    }
}
