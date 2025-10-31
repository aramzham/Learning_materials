var cache = new MyMemoryCache();

// set
cache.Set("foo", "foo");
cache.Set("bar", "bar");
cache.Set("abc", null);

// get
var foo = cache.Get("foo");
var bar = cache.Get("bar");

Console.WriteLine($"foo: {foo}");
Console.WriteLine($"bar: {bar}");

// remove
cache.Remove("foo");
foo = cache.Get("foo");
Console.WriteLine($"foo: {foo}, exists - {cache.TryGetValue("foo", out _)}, is null - {foo is null}");
Console.WriteLine($"bar: {bar}, exists - {cache.TryGetValue("bar", out _)}, is null - {bar is null}");
Console.WriteLine($"abc: {cache.Get("abc")}, exists - {cache.TryGetValue("abc", out _)}, is null - {cache.Get("abc") is null}");


public class  MyMemoryCache
{
    private readonly Dictionary<string, object?> _cache = new();

    public void Set(string key, object? value)
    {
        _cache[key] = value;
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
        return _cache.TryGetValue(key, out value);
    }
}