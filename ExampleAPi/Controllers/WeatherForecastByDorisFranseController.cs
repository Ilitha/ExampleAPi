using Microsoft.AspNetCore.Mvc;
using ExampleAPi.Services;

namespace ExampleAPi.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastByDorisFranseController : ControllerBase
{
    private readonly IWeatherService _weatherService;

    public WeatherForecastByDorisFranseController(IWeatherService weatherService)
    {
        _weatherService = weatherService;
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
            return StatusCode(503, $"Weather service unavailable: {ex.Message}");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }
}
