using System.Reflection;

var type = typeof(OurClass);
var bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;

var ctors = type.GetConstructors(bindingFlags);

var parameterlessConstructor = ctors.FirstOrDefault(ctor => ctor.GetParameters().Length == 0);
var instance = parameterlessConstructor.Invoke(null);
Console.WriteLine();

var parameterizedConstructor = ctors.FirstOrDefault(ctor => ctor.GetParameters().Length == 2);
var instance2 = parameterizedConstructor.Invoke([122333, "Damian Lillard"]);
Console.WriteLine();

// BREAKING RULES!!
var privateConstructor = ctors.FirstOrDefault(ctor => ctor.IsPrivate);
var instance3 = privateConstructor.Invoke([09876, "Giannis Antetokounmpo", Math.PI]);
Console.WriteLine();

// creating objects like this is much faster
// var instance4 = new OurClass(2, "");

public sealed class OurClass
{
    // parameterless
    public OurClass() : this(32, "Hello, World!")
    {
        Console.WriteLine("Default ctor called.");
    }

    // with parameters
    public OurClass(int someInt, string someString) : this(someInt, someString, 1.335)
    {
        Console.WriteLine("Ctor with 2 parameters called.");
    }
    
    private OurClass(int someInt, string someString, double anotherNumber)
    {
        Console.WriteLine("Private ctor called");

        Console.WriteLine($"someInt = {someInt}");
        Console.WriteLine($"someString = {someString}");
        Console.WriteLine($"anotherNumber = {anotherNumber}");
    }
}