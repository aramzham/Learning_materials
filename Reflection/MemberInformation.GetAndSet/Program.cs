using System.Reflection;

var instance = new OurExampleType();
var type = instance.GetType();

var fields = type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static);
Console.WriteLine($"There are {fields.Length} fields.");
foreach (var field in fields)
{
    Console.WriteLine($"\tField: {field}");
}

var properties = type.GetProperties(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static);
Console.WriteLine($"There are {properties.Length} properties.");
foreach (var property in properties)
{
    Console.WriteLine($"\tProperty: {property}");
}

var readonlyField = fields.First(f => f.Name == "_readonlyField");
var readonlyFieldValue = readonlyField.GetValue(instance);
Console.WriteLine($"Readonly field value: {readonlyFieldValue}");

var staticField = fields.First(x => x.IsStatic);
var staticFieldValue = staticField.GetValue(null);
Console.WriteLine($"Static field value: {staticFieldValue}");

var readWriteAutoProperty = properties.First(x => x.Name == "ReadWriteAutoProperty");
var readWriteAutoPropertyValue = readWriteAutoProperty.GetValue(instance);
Console.WriteLine($"ReadWriteAutoProperty value: {readWriteAutoPropertyValue}");

readWriteAutoProperty.SetValue(instance, 3);
readWriteAutoPropertyValue = readWriteAutoProperty.GetValue(instance);
Console.WriteLine($"ReadWriteAutoProperty value: {readWriteAutoPropertyValue}");

var readOverReadWriteField = properties.First(x => x.Name == "ReadOverReadWriteFiled");
var readOverReadWriteFieldValue = readOverReadWriteField.GetValue(instance);
Console.WriteLine($"ReadOverReadWriteFiled value: {readOverReadWriteFieldValue}");
// readOverReadWriteField.SetValue(instance, 4); // this throws exception as the property is read-only, no cheating!!

var fullOverReadWriteField = properties.First(x => x.Name == "FullOverReadWriteField");
var fullOverReadWriteFieldValue = fullOverReadWriteField.GetValue(instance);
Console.WriteLine($"FullOverReadWriteField value: {fullOverReadWriteFieldValue}");

fullOverReadWriteField.SetValue(instance, 5);
fullOverReadWriteFieldValue = fullOverReadWriteField.GetValue(instance);
Console.WriteLine($"FullOverReadWriteField value: {fullOverReadWriteFieldValue}");

var readWriteField = fields.First(x => x.Name == "_readWriteField");
var readWriteFieldValue = readWriteField.GetValue(instance);
Console.WriteLine($"ReadWriteField value: {readWriteFieldValue}");

// DANGER!! we are changing a value of a PRIVATE, READONLY field, 2 breaks of rules
readonlyField.SetValue(instance, 6);
readonlyFieldValue = readonlyField.GetValue(instance);
Console.WriteLine($"Readonly field value: {readonlyFieldValue}");

// not possible to change static field value 
// staticField.SetValue(null, "some other value");
// staticFieldValue = staticField.GetValue(null);
// Console.WriteLine($"Static field value: {staticFieldValue}");

public sealed class OurExampleType
{
    private static readonly string _PrivateStaticField = "PrivateStaticField";

    private readonly int _readonlyField = 1;
    private int _readWriteField = 2;

    public int ReadWriteAutoProperty { get; set; }

    public int ReadOverReadWriteFiled => _readWriteField;

    public int FullOverReadWriteField
    {
        get => _readWriteField;
        set => _readWriteField = value;
    }    
}



