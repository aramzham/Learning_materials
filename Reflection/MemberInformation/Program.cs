using System.Reflection;

var type = typeof(OurExampleType);

var constructors = type.GetConstructors(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
Console.WriteLine($"There are {constructors.Length} constructors.");
foreach (var constructor in constructors)
{
    Console.WriteLine($"\tConstructor: {constructor}");
}

var events = type.GetEvents();
Console.WriteLine($"There are {events.Length} events.");
foreach (var eventInfo in events)
{
    Console.WriteLine($"\tEvent: {eventInfo}");
}

var properties = type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
Console.WriteLine($"There are {properties.Length} properties.");
foreach (var property in properties)
{
    Console.WriteLine($"\tProperty: {property}");
}

properties.Where(x => x is { CanRead: true, CanWrite: true }).ToList().ForEach(x => Console.WriteLine($"\t\tProperty with getter and setter: {x}"));
properties.Where(x => x is { CanRead: true, CanWrite: false }).ToList().ForEach(x => Console.WriteLine($"\t\tProperty with getter only: {x}"));


// Get all methods including inherited ones
var methods = type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);

// Print the total count
Console.WriteLine($"There are {methods.Length} methods.");

// Print details for each method
foreach (var method in methods)
{
    var accessibility = method.IsPublic ? "public" : "private";
    var isStatic = method.IsStatic ? "static " : "";
    Console.WriteLine($"\tMethod: {accessibility} {isStatic}{method.Name}");
}

var fields = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
Console.WriteLine($"There are {fields.Length} fields.");
foreach (var field in fields)
{
    Console.WriteLine($"\tField: {field}");
}


public sealed class OurExampleType
{
    public static string _StaticField;
    
    private readonly int _someField;

    public OurExampleType(int someField)
    {
        _someField = someField;
    }

    private OurExampleType()
    {
        Console.WriteLine("This constructor is private.");
    }

    public static int StaticProperty { get; set; }

    public event EventHandler? SomeEvent;
    
    public string? SomeProperty { get; set; }
    private int PrivateProperty { get; }

    private static  void PrivateStaticMethod()
    {
    }
    
    public void DoSomething()
    {
    }
}