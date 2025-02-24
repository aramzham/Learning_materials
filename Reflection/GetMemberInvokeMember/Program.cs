using System.Reflection;

var searchBindingFlags = BindingFlags.Public | BindingFlags.Instance;
var type = typeof(MyType);

var myConstructorMember = type.GetMember(".ctor", searchBindingFlags).Single();
Console.WriteLine($"My ctor member : {myConstructorMember.Name}");

var myMethodMember = type.GetMember("MyMethod", searchBindingFlags).Single();
Console.WriteLine($"My method member : {myMethodMember.Name}");

var myMessageMember = type.GetMember("MyMessage", searchBindingFlags).Single();
Console.WriteLine($"My message member : {myMessageMember.Name}");

var instance = (MyType?)type.InvokeMember(
    name: null,
    invokeAttr: searchBindingFlags | BindingFlags.CreateInstance,
    binder: null,
    target: null,
    args: ["Hello world!"]);

type.InvokeMember(
    name: "MyMethod",
    invokeAttr: searchBindingFlags | BindingFlags.InvokeMethod,
    binder: null,
    target: instance, // we need instance here
    args: null);

var message = (string?)type.InvokeMember(
    name: "MyMessage",
    invokeAttr: searchBindingFlags | BindingFlags.GetProperty,
    binder: null,
    target: instance, // we need instance here
    args: null);
Console.WriteLine($"My Message = {message}");

public sealed class MyType(string myMessage)
{
    public string MyMessage => myMessage;

    public void MyMethod()
    {
        Console.WriteLine(myMessage);
    }
}