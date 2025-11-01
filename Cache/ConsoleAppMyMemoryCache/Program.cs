using System.Collections.Concurrent;

var cache = new MyMemoryCache();

// set
cache.Set("foo", "foo", new MyMemoryCacheEntryOptions()
{
    Duration = TimeSpan.FromSeconds(5)
});
cache.Set("bar", "bar", new MyMemoryCacheEntryOptions()
{
    Duration = TimeSpan.FromSeconds(2)
});
cache.Set("abc", null);

// get
var foo = cache.Get("foo");
var bar = cache.Get("bar");

Console.WriteLine("GET SET");
Console.WriteLine($"foo: {foo}");
Console.WriteLine($"bar: {bar}");

// remove
cache.Remove("foo");
foo = cache.Get("foo");
Console.WriteLine("REMOVE");
Console.WriteLine($"foo: {foo}, exists - {cache.TryGetValue("foo", out _)}, is null - {foo is null}");
Console.WriteLine($"bar: {bar}, exists - {cache.TryGetValue("bar", out _)}, is null - {bar is null}");
Console.WriteLine($"abc: {cache.Get("abc")}, exists - {cache.TryGetValue("abc", out _)}, is null - {cache.Get("abc") is null}");

// expiration
await Task.Delay(TimeSpan.FromSeconds(5));
Console.WriteLine("EXPIRATION");
Console.WriteLine($"foo: {cache.Get("foo")}, exists - {cache.TryGetValue("foo", out _)}, is null - {cache.Get("foo") is null}");
Console.WriteLine($"bar: {cache.Get("bar")}, exists - {cache.TryGetValue("bar", out _)}, is null - {cache.Get("bar") is null}");

// validation
Console.WriteLine("VALIDATION");
try
{
    cache.Set(null, "foo");
    Console.WriteLine("FAIL");
}
catch (ArgumentNullException)
{
    Console.WriteLine("Validation works");
}

public class MyMemoryCache(MyMemoryCacheOptions? options = null)
{
    private readonly MyMemoryCacheOptions _options = options ?? new MyMemoryCacheOptions();
    private readonly ConcurrentDictionary<string, MyMemoryCacheEntry> _cache = [];

    public void Set(string key, object? value, MyMemoryCacheEntryOptions? entryOptions = null)
    {
        Validate(key);
        
        _cache[key] = new MyMemoryCacheEntry(value, DateTimeOffset.Now.Add(entryOptions?.Duration ?? _options.DefaultDuration));
    }

    public object? Get(string key)
    {
        return TryGetValue(key, out var value) ? value : null;
    }

    public void Remove(string key)
    {
        Validate(key);
        
        _ = _cache.TryRemove(key, out _);
    }

    public bool TryGetValue(string key, out object? value)
    {
        if (_cache.TryGetValue(key, out var entry))
        {
            if (entry.AbsoluteExpiration > DateTimeOffset.Now)
            {
                value = entry.Value;
                return true;
            }

            Remove(key);
        }

        value = null;
        return false;
    }

    private void Validate(string key)
    {
        ArgumentNullException.ThrowIfNull(key);
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
}

public class MyMemoryCacheEntryOptions
{
    public TimeSpan? Duration { get; set; }
}