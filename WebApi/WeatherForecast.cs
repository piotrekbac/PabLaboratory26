namespace WebApi;

// Piotr Bacior - WSEI Kraków

// Klasa przykładowa — generowana automatycznie przez szablon projektu
public class WeatherForecast
{
    public DateOnly Date { get; set; }

    public int TemperatureC { get; set; }

    // Właściwość wyliczana — przeliczanie Celsjuszy na Fahrenheity
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

    public string? Summary { get; set; }
}