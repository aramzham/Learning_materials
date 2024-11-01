namespace Minimal.Api.Services;

public class SimpleService : ISimpleService
{
    public void DoSomething()
    {
        // do something
        Console.WriteLine("SimpleService.DoSomething() called");
    }

    public string GetMessage()
    {
        return "Hello from SimpleService";
    }
}