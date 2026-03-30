using Microsoft.AspNetCore.Mvc;
using ExampleAPi.Services;

namespace ExampleAPi.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastByDorisFranseController : ControllerBase
{
    private readonly IWeatherService _weatherService;

    private readonly ILogger<WeatherForecastByDorisFranseController> _logger;

    public WeatherForecastByDorisFranseController(IWeatherService weatherService, ILogger<WeatherForecastByDorisFranseController> logger)
    {
        _weatherService = weatherService;
        _logger = logger;
    }

    [HttpGet(Name = "GetWeatherForecastByDorisFranse")]
    public async Task<ActionResult<IEnumerable<WeatherForecast>>> Get(
        [FromQuery] double latitude = 52.52, 
        [FromQuery] double longitude = 13.41)
    {
        try
        {
            var forecast = await _weatherService.GetForecastAsync(latitude, longitude);
            return Ok(forecast);
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "Weather service unavailable");
            return StatusCode(503, $"Weather service unavailable: {ex.Message}");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while fetching the weather forecast");
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }
}
