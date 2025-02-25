[AttributeUsage(AttributeTargets.Class)]
public sealed class CustomClassAttribute : Attribute
{
    public CustomClassAttribute(string name)
    {
        Name = name;
    }

    public string Name { get; }
}

[AttributeUsage(AttributeTargets.Method)]
public sealed class CustomMethodAttribute : Attribute;

[AttributeUsage(AttributeTargets.Parameter)]
public sealed class CustomParameterAttribute : Attribute;

[CustomClass("OurClass")]
public class OurDecoratedClass
{
    // [CustomClass("OurMethod")] // this won't work!!
    [CustomMethod]
    public void OurDecoratedMethod([CustomParameter] int parameter)
    {
        Console.WriteLine("Hello from decorated method");
    }
}