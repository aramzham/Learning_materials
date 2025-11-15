using Microsoft.Extensions.Caching.Hybrid;

namespace ParallelGet;

public static class HybridCacheExtensionMethods
{
    private static readonly HybridCacheEntryOptions _TryGetAsyncEntryOptions = new()
        { Flags = HybridCacheEntryFlags.DisableLocalCacheWrite | HybridCacheEntryFlags.DisableDistributedCacheWrite };

    // problems:
    // - non deterministic
    // - wrong behavior with L2 to L1 copy
    public static async ValueTask<(bool Found, T? Value)> TryGetAsync<T>(this HybridCache cache, string key)
    {
        var found = true;
        var value = await cache.GetOrCreateAsync<T>(key, _ =>
        {
            found = false;
            return ValueTask.FromResult(default(T))!;
        }, _TryGetAsyncEntryOptions);

        return (found, value);
    }
}