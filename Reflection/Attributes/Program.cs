using System.Reflection;
using System.Runtime.CompilerServices;

var typesThatHaveMyFancyAttribute = Assembly.GetExecutingAssembly().GetTypes()
    .Where(t => t.GetCustomAttribute<MyFancyAttribute>() != null)
    .ToList();
Console.WriteLine($"Found {typesThatHaveMyFancyAttribute.Count} types with MyFancyAttribute");
foreach (var t in typesThatHaveMyFancyAttribute)
{
    var attribute = t.GetCustomAttribute<MyFancyAttribute>();
    Console.WriteLine($"Type {t.Name} has MyFancyAttribute with name {attribute.Name}");
}

var type = typesThatHaveMyFancyAttribute.First();

var methodsThatHaveMyFancyAttribute = type.GetMethods()
    .Where(m => m.GetCustomAttributes<MyFancyAttribute>().Any()).ToList();
Console.WriteLine($"Found {methodsThatHaveMyFancyAttribute.Count} methods with MyFancyAttribute");
foreach (var m in methodsThatHaveMyFancyAttribute)
{
    var attribute = m.GetCustomAttribute<MyFancyAttribute>();
    Console.WriteLine($"Method {m.Name} has MyFancyAttribute with name {attribute.Name}");
}

var methodsWithCallerMemberNameParams = type.GetMethods()
    .Where(x => x.GetCustomAttributes<MyFancyAttribute>().Any())
    .Where(m => m.GetParameters().Any(p => p.GetCustomAttributes<CallerMemberNameAttribute>().Any())).ToList();
Console.WriteLine($"Found {methodsWithCallerMemberNameParams.Count} methods with CallerMemberNameAttribute");
foreach (var method in methodsWithCallerMemberNameParams)
{
    Console.WriteLine($"Method: {method.Name}");
    var parameter = method.GetParameters().Single(p => p.GetCustomAttribute<CallerMemberNameAttribute>() != null);
    Console.WriteLine($"Parameter: {parameter.Name}");
}

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public sealed class MyFancyAttribute : Attribute
{
    public MyFancyAttribute(string name)
    {
        Name = name;
    }

    public string Name { get; }
}

[MyFancy("this class has been annotated!")]
public sealed class MyType
{
    [MyFancy("this method has been annotated!")]
    public void MyMethod([CallerMemberName] string? name = null)
    {
        Console.WriteLine("Hello, World!");
    }
    
    [MyFancy("this ALSO has a fancy name!")]
    public void MyOtherMethod()
    {
        Console.WriteLine("Hello, World!");
    }

    public void NotFancyAtAll(string? name = null)
    {
    }
}