using System.Net.Http.Json;
using Weather.Cli.Models;

namespace Weather.Cli;

public class Program
{
    public static async Task Main(string[] args)
    {
        if (args.Length == 0) return;
        var city = args[0];

        var apiKey = Environment.GetEnvironmentVariable("OpenWeatherApiKey");
        var url = $"http://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKey}&units=metric";
        HttpClient? client = new HttpClient();
        var response = await client.GetAsync(url);

        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            Console.WriteLine($"No weather found for {city}");
            return;
        }

        var currentWeather = await response.Content.ReadFromJsonAsync<WeatherResponse?>();

        if (currentWeather != null)
        {
            Console.WriteLine($"Current Weather in: {city}");
            Console.WriteLine($"City: {city}");
            Console.WriteLine($"Country: {currentWeather?.Sys?.Country}");
            Console.WriteLine($"Weather: {currentWeather?.Weather?[0].Description}");
            Console.WriteLine($"Temperature: {currentWeather?.Main?.Temp}°C");
            Console.WriteLine($"Feels Like: {currentWeather?.Main?.Feels_Like}°C");
            Console.WriteLine($"Humidity: {currentWeather?.Main?.Humidity}%");
            Console.WriteLine($"Pressure: {currentWeather?.Main?.Pressure}hPa");
            Console.WriteLine($"Wind: {currentWeather?.Wind?.Speed}m/s, {currentWeather?.Wind?.Deg}°");
            Console.WriteLine($"Clouds: {currentWeather?.Clouds?.All}%");
            Console.WriteLine($"Sunrise: {ConvertToLocalTime(currentWeather.Sys.Sunrise)}");
            Console.WriteLine($"Sunset: {ConvertToLocalTime(currentWeather.Sys.Sunset)}");
        }
    }

    private static DateTime ConvertToLocalTime(double unixTimeStamp)
    {
        // Unix timestamp is seconds past epoch
        DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        dateTime = dateTime.AddSeconds(unixTimeStamp).ToLocalTime();
        return dateTime;
    }
}