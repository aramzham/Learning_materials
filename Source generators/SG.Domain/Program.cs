// See https://aka.ms/new-console-template for more information

Console.WriteLine("Hello, World!");

var list = new List<A>();
var a = new A();
for (int i = 0; i < 10; i++)
{
    a.aaa = i;
    list.Add(a);
}
foreach (var item in list)
{
    Console.WriteLine(item);
}

public class A
{
    public int aaa = 0;
    override public string ToString()
    {
        return $"aaa = {aaa}";
    }
}