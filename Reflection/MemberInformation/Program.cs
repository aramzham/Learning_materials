var type = typeof(OurExampleType);

var constructors = type.GetConstructors();
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

var properties = type.GetProperties();
Console.WriteLine($"There are {properties.Length} properties.");
foreach (var property in properties)
{
    Console.WriteLine($"\tProperty: {property}");
}

properties.Where(x => x is { CanRead: true, CanWrite: true }).ToList().ForEach(x => Console.WriteLine($"\t\tProperty with getter and setter: {x}"));
properties.Where(x => x is { CanRead: true, CanWrite: false }).ToList().ForEach(x => Console.WriteLine($"\t\tProperty with getter only: {x}"));

var methods = type.GetMethods();
Console.WriteLine($"There are {methods.Length} methods.");
foreach (var method in methods)
{
    Console.WriteLine($"\tMethod: {method}");
}

var fields = type.GetFields();
Console.WriteLine($"There are {fields.Length} fields.");
foreach (var field in fields)
{
    Console.WriteLine($"\tField: {field}");
}


public sealed class OurExampleType
{
    private readonly int _someField;

    public OurExampleType(int someField)
    {
        _someField = someField;
    }
    
    public event EventHandler? SomeEvent;
    
    public string? SomeProperty { get; set; }
    public int AnotherProperty { get; }

    public void DoSomething()
    {
    }
}