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