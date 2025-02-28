using System.Reflection;
using Generics;

var genericTypeA = typeof(GenericTypeA<,,>);

var genericTypes = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.IsGenericType);

Console.WriteLine("The generic types in this assembly are:");
foreach (var type in genericTypes)
{
    Console.WriteLine(type.Name);

    Console.WriteLine("\tType parameters:");
    foreach (var typeParameter in type.GetGenericArguments())
    {
        Console.WriteLine($"\t\t{typeParameter.Name}");
        
        var constraints = typeParameter.GetGenericParameterConstraints();
        if (constraints.Length > 0)
        {
            Console.WriteLine("\t\t\tConstraints:");
            foreach (var constraint in constraints)
            {
                Console.WriteLine($"\t\t\t\t must be {constraint.Name}");
            }
        }
    }

    Console.WriteLine("\tMembers:");
    foreach (var member in type.GetMembers())
    {
        Console.WriteLine($"\t\t{member.Name}");
        
        if (member is MethodInfo method)
        {
            Console.WriteLine("\t\t\tReturn type:");
            Console.WriteLine($"\t\t\t\t{method.ReturnType.Name}");

            var methodParameters = method.GetParameters();
            foreach (var methodParameter in methodParameters)
            {
                Console.WriteLine($"\t\t\t\t{methodParameter.Name} : {methodParameter.ParameterType.Name}");

                if (methodParameter.ParameterType.IsGenericType)
                {
                    var constraints = methodParameter.ParameterType.GetGenericParameterConstraints();
                    if (constraints.Length > 0)
                    {
                        Console.WriteLine("\t\t\t\t\tConstraints:");
                        foreach (var constraint in constraints)
                        {
                            Console.WriteLine($"\t\t\t\t\t\t must be {constraint.Name}");
                        }
                    }
                }
            }
        }
    }
}