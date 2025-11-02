using System.Collections.Concurrent;

var cache = new MyMemoryCache();

// set
cache.Set("foo", 123, new MyMemoryCacheEntryOptions()
{
    Duration = TimeSpan.FromSeconds(5)
});
cache.Set("bar", "bar", new MyMemoryCacheEntryOptions()
{
    Duration = TimeSpan.FromSeconds(2)
});
cache.Set<object?>("abc", null);

// get
var foo = cache.Get<int?>("foo");
var bar = cache.Get<string?>("bar");

Console.WriteLine("GET SET");
Console.WriteLine($"foo: {foo}");
Console.WriteLine($"bar: {bar}");

// remove
cache.Remove("foo");
foo = cache.Get<int?>("foo");
Console.WriteLine("REMOVE");
Console.WriteLine($"foo: {foo}, exists - {cache.TryGetValue<int?>("foo", out _)}, is null - {foo is null}");
Console.WriteLine($"bar: {bar}, exists - {cache.TryGetValue<string?>("bar", out _)}, is null - {bar is null}");
Console.WriteLine($"abc: {cache.Get<object?>("abc")}, exists - {cache.TryGetValue<object?>("abc", out _)}, is null - {cache.Get<object?>("abc") is null}");

// expiration
await Task.Delay(TimeSpan.FromSeconds(5));
Console.WriteLine("EXPIRATION");
Console.WriteLine($"foo: {cache.Get<int?>("foo")}, exists - {cache.TryGetValue<int?>("foo", out _)}, is null - {cache.Get<int?>("foo") is null}");
Console.WriteLine($"bar: {cache.Get<string?>("bar")}, exists - {cache.TryGetValue<string?>("bar", out _)}, is null - {cache.Get<string?>("bar") is null}");

// validation
Console.WriteLine("VALIDATION");
try
{
    cache.Set<string?>(null, "foo");
    Console.WriteLine("FAIL");
}
catch (ArgumentNullException)
{
    Console.WriteLine("Validation works");
}

// eviction
cache.Set("foo1", 987, new MyMemoryCacheEntryOptions()
{
    Duration = TimeSpan.FromSeconds(1)
});
cache.Set("bar1", "bar", new MyMemoryCacheEntryOptions()
{
    Duration = TimeSpan.FromSeconds(1)
});

await Task.Delay(TimeSpan.FromSeconds(1.5));

// disposal
cache.Dispose();
try
{
    cache.Set("foo", "foo");
    Console.WriteLine("FAIL");
}
catch (ObjectDisposedException)
{
    Console.WriteLine("Disposal works");
}

public sealed class MyMemoryCache : IDisposable
{
    private readonly MyMemoryCacheOptions _options;
    private ConcurrentDictionary<string, MyMemoryCacheEntry> _cache = [];
    private bool _disposedValue;
    private CancellationTokenSource _cts = new();

    public MyMemoryCache(MyMemoryCacheOptions? options = null)
    {
        _options = options ?? new MyMemoryCacheOptions();
        
        Task.Run(async () =>
        {
            while (!_cts.IsCancellationRequested)
            {
                try
                {
                    await Task.Delay(_options.EvictionInterval, _cts.Token);
                    Console.WriteLine("Evicting expired entries");
                    EvictExpired();
                }
                catch (OperationCanceledException)
                {
                    return;
                }
            }
        });
    }
    
    public void Set<TValue>(string key, TValue? value, MyMemoryCacheEntryOptions? entryOptions = null)
    {
        CheckDisposed();
        Validate(key);
        
        _cache[key] = new MyMemoryCacheEntry(value, DateTimeOffset.Now.Add(entryOptions?.Duration ?? _options.DefaultDuration));
    }

    public TValue? Get<TValue>(string key)
    {
        return TryGetValue<TValue>(key, out var value) ? value : default;
    }

    public void Remove(string key)
    {
        CheckDisposed();
        Validate(key);
        
        _ = _cache.TryRemove(key, out _);
    }

    public bool TryGetValue<TValue>(string key, out TValue? value)
    {
        CheckDisposed();

        if (_cache.TryGetValue(key, out var entry))
        {
            if (entry.AbsoluteExpiration > DateTimeOffset.Now)
            {
                value = (TValue?)entry.Value;
                return true;
            }

            Remove(key);
        }

        value = default;
        return false;
    }
    
    private void EvictExpired()
    {
        foreach (var key in _cache.Keys)
        {
            if (_cache[key].AbsoluteExpiration < DateTimeOffset.Now)
            {
                Remove(key);
            }
        }
    }

    private void Validate(string key)
    {
        ArgumentNullException.ThrowIfNull(key);
    }
    
    private void CheckDisposed()
    {
        if (_disposedValue)
        {
            throw new ObjectDisposedException(nameof(MyMemoryCache));
        }
    }

    private void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            if (disposing)
            {
                _cts.Cancel();
                _cts.Dispose();
                _cts = null!;
            }

            _cache = null!; // force null for garbage collection
            _disposedValue = true;
        }
    }
    
    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}

public class MyMemoryCacheEntry(object? value, DateTimeOffset absoluteExpiration)
{
    public object? Value { get; } = value;
    public DateTimeOffset AbsoluteExpiration { get; } = absoluteExpiration;
}

public class MyMemoryCacheOptions
{
    public TimeSpan DefaultDuration { get; set; } = TimeSpan.FromMinutes(5);
    public TimeSpan EvictionInterval { get; set; } = TimeSpan.FromMinutes(5);
}

public class MyMemoryCacheEntryOptions
{
    public TimeSpan? Duration { get; set; }
}