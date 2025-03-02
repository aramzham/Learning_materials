using System.Runtime.CompilerServices;

var instance = PrivateCtor(42);
var privateProperty = GetPrivateProperty(instance);
Console.WriteLine($"Private property: {privateProperty}");

ref var referenceToPrivateField = ref GetPrivateField(instance);

ref var referenceToStaticPrivateField = ref GetSetStaticPrivateField(null);
Console.WriteLine($"Static private field: {referenceToStaticPrivateField}");

PrivateMethod(instance);
StaticPrivateMethod(null);

referenceToPrivateField = 987654; // this will change the value of private field because it holds its reference
PrivateMethod(instance);

[UnsafeAccessor(UnsafeAccessorKind.Constructor)]
static extern OurType PrivateCtor(int i);

[UnsafeAccessor(UnsafeAccessorKind.Method, Name = "PrivateMethod")]
static extern void PrivateMethod(OurType instance);

[UnsafeAccessor(UnsafeAccessorKind.StaticMethod, Name = "StaticPrivateMethod")]
static extern void StaticPrivateMethod(OurType instance);

[UnsafeAccessor(UnsafeAccessorKind.Field, Name = "_privateField")]
static extern ref int GetPrivateField(OurType instance);

[UnsafeAccessor(UnsafeAccessorKind.StaticField, Name = "_staticPrivateField")]
static extern ref int GetSetStaticPrivateField(OurType instance);

[UnsafeAccessor(UnsafeAccessorKind.Method, Name = "get_PrivateProperty")]
static extern int GetPrivateProperty(OurType instance);

public class OurType
{
    private static int _staticPrivateField = 123456;

    private int _privateField;

    private OurType(int i)
    {
        _privateField = i;
    }

    private static void StaticPrivateMethod()
    {
        Console.WriteLine($"Static private: {_staticPrivateField}");
    }
    
    private void PrivateMethod()
    {
        Console.WriteLine($"Instance private: {_privateField}");
    }

    private static int StaticPrivateProperty => _staticPrivateField;

    private int PrivateProperty => _privateField;
}