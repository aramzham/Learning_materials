using System.Diagnostics;

namespace ParallelFor;

public abstract class Food
{
    private TimeSpan _cookingTime;
    
    public string Name { get; }

    protected Food(TimeSpan cookingTime)
    {
        _cookingTime = cookingTime;
        Name = this.GetType().Name;
    }
    
    public async Task Cook(int orderNumber, CancellationToken token)
    {
        Trace.WriteLine($"Cooking {Name} for order {orderNumber} on {Environment.CurrentManagedThreadId}");
        await Task.Delay(_cookingTime, token);
        Trace.WriteLine($"{Name} completed for order {orderNumber} on {Environment.CurrentManagedThreadId}!");
    }
    
    public override string ToString() => Name;
}

public class Risotto() : Food(TimeSpan.FromSeconds(5));
public class Salad() : Food(TimeSpan.FromSeconds(2));
public class Pasta() : Food(TimeSpan.FromSeconds(3));
public class Pizza() : Food(TimeSpan.FromSeconds(8));
public class Lasagna() : Food(TimeSpan.FromSeconds(10));