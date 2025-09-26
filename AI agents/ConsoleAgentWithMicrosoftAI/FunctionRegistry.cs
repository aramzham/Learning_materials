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
            });
            
        var wardrobeService = sp.GetRequiredService<WardrobeService>();

        var getClothesFn = typeof(WardrobeService).GetMethod(nameof(WardrobeService.GetClothes), []);

        yield return AIFunctionFactory.Create(getClothesFn, wardrobeService, new AIFunctionFactoryOptions()
        {
            Name = "get_wardrobe_clothes",
            Description = "List all the clothing i have in my wardrobe"
        });
    }
}