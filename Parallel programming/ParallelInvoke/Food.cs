using System.Diagnostics;

namespace ParallelInvoke;

public abstract class Food
{
    private TimeSpan _cookingTime;
    
    public string Name { get; }

    protected Food(TimeSpan cookingTime)
    {
        _cookingTime = cookingTime;
        Name = this.GetType().Name;
    }
    
    public void Cook()
    {
        Trace.WriteLine($"Start cooking {Name}");
        Thread.Sleep(_cookingTime);
        Trace.WriteLine($"{Name} is ready!");
    }
    
    public override string ToString() => Name;
}

public class Risotto() : Food(TimeSpan.FromSeconds(5));
public class Salad() : Food(TimeSpan.FromSeconds(2));
public class Pasta() : Food(TimeSpan.FromSeconds(3));
public class Pizza() : Food(TimeSpan.FromSeconds(8));
public class Lasagna() : Food(TimeSpan.FromSeconds(10));