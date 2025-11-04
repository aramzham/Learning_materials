using Microsoft.Extensions.Caching.Hybrid;
using Microsoft.Extensions.DependencyInjection;
using ParallelGet;
using ZiggyCreatures.Caching.Fusion;

// setup di
var services = new ServiceCollection();
services.AddHybridCache();
// services.AddFusionCache().AsHybridCache(); // magic

// get the cache
var serviceProvider = services.BuildServiceProvider();
var cache = serviceProvider.GetRequiredService<HybridCache>();

// test (sequential)
Console.WriteLine("Sequential");
for (int i = 0; i < 10; i++)
{
    var value = await cache.TryGetAsync<int>("test");
    Console.WriteLine($"value = {value.Value}, found = {value.Found.ToString().ToUpper()}");
}
Console.WriteLine("Done");

// test (parallel)
Console.WriteLine("Parallel");
await Parallel.ForEachAsync(Enumerable.Range(0, 10), async (_, _) =>
{
    var value = await cache.TryGetAsync<int>("test");
    Console.WriteLine($"value = {value.Value}, found = {value.Found.ToString().ToUpper()}");
});