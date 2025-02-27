using System.Reflection;

var type1 = typeof(CustomType1);
// CustomType1 instance1 = Activator.CreateInstance(type1);
object? instance1 = Activator.CreateInstance(type1);
CustomType1 instance1a = Activator.CreateInstance<CustomType1>();

Console.WriteLine($"Is instance1 customtype1: {instance1 is CustomType1}");
Console.WriteLine($"Is instance1a customtype1: {instance1a is CustomType1}");

var type2 = typeof(CustomType2);
object? instance2 = Activator.CreateInstance(type2, "parameter1");
// object? instance2 = Activator.CreateInstance(type2, "parameter1", 123); // parameter mismatch

var type3 = typeof(CustomType3);
// object? instance3 = Activator.CreateInstance(type3, "parameter1", 123); // ctor is private!!
object? instance3 = Activator.CreateInstance(type3, BindingFlags.NonPublic | BindingFlags.Instance, null, ["parameter1", 123
], null);
var instance3a = Activator.CreateInstance(type3, nonPublic: true);

public sealed class CustomType1
{
    public CustomType1()
    {
        Console.WriteLine("CustomType1 constructor");
    }
}

public sealed class CustomType2
{
    public CustomType2(string parameter1)
    {
        Console.WriteLine("CustomType2 constructor");
        Console.WriteLine($"\tParameter1: {parameter1}");
    }
}

public sealed class CustomType3
{
    private CustomType3()
    {
        Console.WriteLine("CustomType3 constructor (private) without parameters");
    }
    
    private CustomType3(string parameter1, int parameter2)
    {
        Console.WriteLine("CustomType3 constructor");
        Console.WriteLine($"\tParameter1: {parameter1}");
        Console.WriteLine($"\tParameter2: {parameter2}");
    }
}