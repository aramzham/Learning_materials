namespace Generics;

public sealed class SomeImplementation(string input) : ISomeInterface
{
    public void SomeMethod()
    {
        Console.WriteLine($"SomeMethod({input})");
    }

    public override string ToString()
    {
        return input;
    }
}

public sealed class  SomeOtherImplementation(int input) : ISomeOtherInterface
{
    public override string ToString()
    {
        return input.ToString();
    }
}

public interface ISomeOtherInterface;

public interface ISomeInterface
{
    public void SomeMethod();
}

public class GenericTypeA<T1, T2, T3> where T2 : ISomeInterface
{
    public void Method1(T1 value1, T2 value2)
    {
        Console.WriteLine($"Method1<{typeof(T1).Name}, {typeof(T2).Name}>({value1}, {value2})");
    }

    public void Method2<TMethodParameter>(TMethodParameter parameter) where TMethodParameter : ISomeInterface
    {
        Console.WriteLine($"Method2<{typeof(TMethodParameter).Name}>({parameter})");
    }
}