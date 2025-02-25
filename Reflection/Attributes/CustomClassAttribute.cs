[AttributeUsage(AttributeTargets.Class)]
public sealed class CustomClassAttribute : Attribute
{
    public CustomClassAttribute(string name)
    {
        Name = name;
    }

    public string Name { get; }
}