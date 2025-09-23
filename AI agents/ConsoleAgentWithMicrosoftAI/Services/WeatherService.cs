using System.Text.Json;

namespace ConsoleAgentWithMicrosoftAI.Services;

public class WeatherService(string apiKey)
{
    private readonly HttpClient _httpClient = new();

    public async Task<string[]> GetWeatherInCity(string city, CancellationToken ct)
    {
        var url = $"http://api.weatherapi.com/v1/current.json?key={apiKey}&q={Uri.EscapeDataString(city)}&aqi=yes";
        var response = await _httpClient.GetAsync(url, ct);
        var responseContent = await response.Content.ReadAsStringAsync(ct);
        if (!response.IsSuccessStatusCode)
            throw new InvalidOperationException(
                $"Error calling Weather API: {response.StatusCode} - {response.Content}");

        using var doc = JsonDocument.Parse(responseContent);
        var descriptionElement = doc.RootElement.GetProperty("current").GetProperty("condition").GetProperty("text");

        return [descriptionElement.GetString()!];
    }
}