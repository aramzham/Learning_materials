namespace ConsoleAgentWithMicrosoftAI.Services;

public class WardrobeService
{
    public Task<string[]> GetClothes()
    {
        return Task.FromResult(new[] {
            "Metalica t-shirt",
            "woolly hat",
            "cardigan",
            "blazer",
            "beany hat",
            "polo",
            "warm sweater",
            "Chicago Bulls jersey"
        });
    }
}