using System.Runtime.CompilerServices;

namespace ExampleAPi.Services;

public interface ILocationService
{
    Task<string?> GetCityAsync(double longitude, double latitude);
}
