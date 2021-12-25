namespace Weather.Cli;

public class WeatherResponse
{
    public Weather[]? Weather { get; set; }

    public Wind? Wind { get; set; }

    public Main? Main { get; set; }

    public Clouds? Clouds { get; set; }

    public Sys? Sys { get; set; }

    public int TimeZone { get; set; }

    public string? Name { get; set; }
}
