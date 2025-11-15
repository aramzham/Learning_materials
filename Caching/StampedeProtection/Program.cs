using System.Collections.Concurrent;
using Microsoft.Extensions.Caching.Hybrid;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using ZiggyCreatures.Caching.Fusion;

var dbCallsCount = 0;

async Task<string> GetDataFromDb()
{
    Interlocked.Increment(ref dbCallsCount);
    await Task.Delay(1000);
    return "data";
}

var services = new ServiceCollection();
// memory
services.AddMemoryCache();
// fusion
services.AddFusionCache();
// hybrid
services.AddHybridCache();
// fusion hybrid adapter
services.AddFusionCache().AsHybridCache();

var serviceProvider = services.BuildServiceProvider();

var memoryCache = serviceProvider.GetRequiredService<IMemoryCache>();
var fusionCache = serviceProvider.GetRequiredService<IFusionCache>();
var hybridCache = serviceProvider.GetRequiredService<HybridCache>();

// simulate cache stampede
var tasks = new ConcurrentBag<Task>();
var requestsCount = 1000;

// memory
// Parallel.For(0, requestsCount, i => tasks.Add(memoryCache.GetOrCreateAsync("key", _ => GetDataFromDb())));

// fusion
// Parallel.For(0, requestsCount, i => tasks.Add(fusionCache.GetOrSetAsync("key", _ => GetDataFromDb()).AsTask()));

// hybrid
// Parallel.For(0, requestsCount, i => tasks.Add(hybridCache.GetOrSetAsync("key", _ => GetDataFromDb()).AsTask()));

// fusion hybrid adapter
Parallel.For(0, requestsCount, i => tasks.Add(hybridCache.GetOrCreateAsync("key", async _ => await GetDataFromDb()).AsTask()));

await Task.WhenAll(tasks);
Console.WriteLine($"Memory cache calls: {dbCallsCount} for {requestsCount} db requests.");