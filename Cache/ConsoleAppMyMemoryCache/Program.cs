var cache = new MyMemoryCache();

// set
cache.Set("foo", "foo", TimeSpan.FromSeconds(2));
cache.Set("bar", "bar", TimeSpan.FromSeconds(5));
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


public class  MyMemoryCache
{
    private readonly Dictionary<string, MyMemoryCacheEntry> _cache = new();

    public void Set(string key, object? value, TimeSpan? absoluteExpirationRelativeToNow = null)
    {
        _cache[key] = new MyMemoryCacheEntry(value, DateTimeOffset.Now.Add(absoluteExpirationRelativeToNow ?? TimeSpan.FromMinutes(5)));
    }

    public object? Get(string key)
    {
        return TryGetValue(key, out var value) ? value : null;
    }

    public void Remove(string key)
    {
        _cache.Remove(key);
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
}

public class MyMemoryCacheEntry(object? value, DateTimeOffset absoluteExpiration)
{
    public object? Value { get; } = value;
    public DateTimeOffset AbsoluteExpiration { get; } = absoluteExpiration;
}