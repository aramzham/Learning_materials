using ConsoleAgentWithMicrosoftAI.Services;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleAgentWithMicrosoftAI;

public static class FunctionRegistry
{
    public static IEnumerable<AITool> GetTools(this IServiceProvider sp)
    {
        var weatherService = sp.GetRequiredService<WeatherService>();

        var getWeatherFn = typeof(WeatherService).GetMethod(nameof(WeatherService.GetWeatherInCity),
            [typeof(string), typeof(CancellationToken)]);

        yield return AIFunctionFactory.Create(
            getWeatherFn,
            weatherService,
            new AIFunctionFactoryOptions()
            {
                Name = "get_weather",
                Description = "Get the current weather descriptions in a specified city"
            }
    );
}
}