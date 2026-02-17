namespace ExampleAPi.Services;

public interface IWeatherService
{
    Task<IEnumerable<WeatherForecast>> GetForecastAsync(double latitude, double longitude); 
}
