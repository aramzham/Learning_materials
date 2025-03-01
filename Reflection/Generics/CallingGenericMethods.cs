using System.Reflection;

namespace Generics;

public class CallingGenericMethods
{
    public void Invoke()
    {
        var genericTypeA = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.IsGenericType)
            .SingleOrDefault(t => t.Name == "GenericTypeA`3");
        var genericMethods = genericTypeA.GetMethods().Where(x => x.IsGenericMethod).ToArray();
        
        var instance = new GenericTypeA<int, SomeImplementation, string>();

        var method1 = instance.GetType().GetMethod("Method1");
        method1.Invoke(instance, [42, new SomeImplementation("input_1")]);
        
        var method2 = instance.GetType().GetMethod("Method2");
        // method2.Invoke(instance, [new SomeOtherImplementation(124)]); // we can't do such late binding because at runtime we don't know the generic type parameter of the method
        method2 = method2.MakeGenericMethod(typeof(SomeImplementation));
        method2.Invoke(instance, [new SomeImplementation("input_2")]);

        genericTypeA = genericTypeA.MakeGenericType(typeof(string), typeof(SomeImplementation), typeof(int));
        var instance2 = (GenericTypeA<string, SomeImplementation, int>)Activator.CreateInstance(genericTypeA)!;
        instance2.Method1("input_3", new SomeImplementation("input_4"));
    }
}