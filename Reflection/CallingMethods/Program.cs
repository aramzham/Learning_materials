using System.Reflection;

var bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static;

var instance = new OurClass();
var type = instance.GetType();

// instance.DoSomething(); // you cannot call a private method on an instance
// BUT!!
// if you can, doesn't mean you should!!

var doSomethingMethod = type.GetMethod("DoSomething", bindingFlags);
doSomethingMethod.Invoke(instance, null);

var doSomethingElse = type.GetMethod("DoSomethingElse", bindingFlags);
doSomethingElse.Invoke(instance, ["Hello World", 42]);

var addNumbers = type.GetMethod("AddNumbers", bindingFlags);
var result = (int)addNumbers.Invoke(null, [2, 10]);
Console.WriteLine($"Result = {result}, half = {result / 2}");


public sealed class OurClass
{
    private void DoSomething()
    {
        Console.WriteLine($"This is the {nameof(DoSomething)} method!");
    }
    
    private void DoSomethingElse(string stringValue, int intValue)
    {
        Console.WriteLine($"This is the {nameof(DoSomethingElse)} method!\n\t with parameters - stringValue = {stringValue}, intValue = {intValue}");
    }

    private static int AddNumbers(int numberA, int numberB) => numberA + numberB;
}