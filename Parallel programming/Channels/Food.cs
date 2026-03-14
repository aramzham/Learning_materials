using System.Diagnostics;

namespace Channels;

public abstract class Food
{
    private readonly TimeSpan _cookingTime;
    
    public string Name { get; }

    protected Food(TimeSpan cookingTime)
    {
        _cookingTime = cookingTime;
        Name = GetType().Name;
    }
    
    public async Task<string> Cook(CancellationToken ct)
    {
        Trace.WriteLine($"Start cooking {Name}");
        await Task.Delay(_cookingTime, ct);
        Trace.WriteLine($"{Name} is ready!");
        
        return Name;
    }
    
    public override string ToString() => Name;
}

public class Risotto() : Food(TimeSpan.FromSeconds(5));
public class Salad() : Food(TimeSpan.FromSeconds(2));
public class Pasta() : Food(TimeSpan.FromSeconds(3));
public class Pizza() : Food(TimeSpan.FromSeconds(8));
public class Lasagna() : Food(TimeSpan.FromSeconds(10));