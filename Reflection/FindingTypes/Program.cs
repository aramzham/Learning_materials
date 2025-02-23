using System.Reflection;

Console.WriteLine("Type by typeof:");
var typeofA = typeof(MyTypeA);
var typeofB = typeof(InternalSealedClass);
Console.WriteLine(typeofA.AssemblyQualifiedName);
Console.WriteLine(typeofB.AssemblyQualifiedName);
Console.WriteLine();

Console.WriteLine("Type by name:");
var typeCByName = Type.GetType("PublicSealedClass");
var typeDByName = Type.GetType("PublicSealedClass+PublicSealedNestedClass");
var typeEByName = Type.GetType("PublicSealedClass+PrivateSealedNestedClass");
Console.WriteLine(typeCByName.AssemblyQualifiedName);
Console.WriteLine(typeDByName.AssemblyQualifiedName);
Console.WriteLine(typeEByName.AssemblyQualifiedName);
Console.WriteLine();

Console.WriteLine("Type by name vs namespace:");
var typeAByName = Type.GetType("MyTypeA");
var typeAFromCoolNamespace = Type.GetType("CoolNamespace.MyTypeA");
Console.WriteLine(typeAByName.AssemblyQualifiedName);
Console.WriteLine(typeAFromCoolNamespace.AssemblyQualifiedName);
Console.WriteLine();

Console.WriteLine("Type from assembly:");
var typeAFromAssembly = Assembly.GetExecutingAssembly().GetType("MyTypeA");
Console.WriteLine(typeAFromAssembly.AssemblyQualifiedName);

Console.WriteLine("All types from assembly");
var allTypesFromAssembly = Assembly.GetExecutingAssembly().GetTypes();
foreach (var type in allTypesFromAssembly)
{
    Console.WriteLine(type.FullName);
}

Console.WriteLine("All abstract types:");
foreach (var t in allTypesFromAssembly.Where(x => x.IsAbstract))
{
    Console.WriteLine(t.Name);
}
Console.WriteLine();

Console.WriteLine("All sealed types:");
foreach (var t in allTypesFromAssembly.Where(x=>x.IsSealed))
{
    Console.WriteLine(t.Name);
}

Console.WriteLine();

Console.WriteLine("All interfaces:");
foreach (var t in allTypesFromAssembly.Where(x=>x.IsInterface))
{
    Console.WriteLine(t.Name);
}

Console.WriteLine();

var assemblies = LoadAssembliesFromDirectory(".");
foreach (var assembly in assemblies)
{
    Console.WriteLine(assembly.FullName);
    foreach (var type in assembly.GetTypes())
    {
        Console.WriteLine($"\t{type.FullName}");
    }
}


static IReadOnlyCollection<Assembly> LoadAssembliesFromDirectory(string directoryPath)
{
    var assemblies = new List<Assembly>();
    var assemblyFiles = Directory.EnumerateFiles(directoryPath, "*.dll");
    foreach (var assemblyFile in assemblyFiles)
    {
        try
        {
            var assembly = Assembly.LoadFrom(assemblyFile);
            assemblies.Add(assembly);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Couldn't load assembly {assemblyFile}\n{e.Message}");
        }
    }

    return assemblies;
}

public class MyTypeA;

internal sealed class InternalSealedClass;

public sealed class PublicSealedClass
{
    public sealed class PublicSealedNestedClass;

    private sealed class PrivateSealedNestedClass;
}

public abstract class AbstractClass;

public sealed class MyDerivedTypeA : AbstractClass;

public interface IMyInterface;

public sealed class MyDerivedTypeB : AbstractClass, IMyInterface;

namespace CoolNamespace
{
    public sealed class MyTypeA;
}