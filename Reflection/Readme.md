C# is a statically typed language that means that at compile time we know the type of variables (it won't change throughout the entire life)
python for example is a dynamically typed language

C# is a strongly typed language meaning there are strong enforcements on typing system (you cannot implicitly assign a double to int)

besides IL code assemblies have metadata about types, type members etc.

reflection is a .net concept (not only specific for C#)
reflection is a technique to work with type metadata

where reflection is used?
- to discover unit tests
- in DI containers
- in ORMs to create type instances
- serialization
- plugins

typeof only works at compile time

when getting all types be aware that interface types have IsAbstract set to true

sealed is opposite to abstract

allTypes.Where(x => x.IsAssignableTo(typeof(IMyInterface))) -> we get all the types that implement our interface

.CanRead on a property means it has a getter
.CanWrite = has a setter

by using binding flags like this
`var fields = type.GetFields(BidingFlags.Public | BidingFlags.NonPublic | BidingFlags.Instance)` we want to "turn on" all the options
| is bitwise or operator

var readonlyField = fields.First(f => f.Name == "_readonlyField");
var readonlyFieldValue = readonlyField.GetValue(instance);
it reads a little bit backwards as on the "instance" we get the value of read-only field

when you set a value on a property, the value of the backing field changes too

calling methods using reflection doesn't give you the compile time safety, you gonna work with objects

.ctor is the name of constructor when your try to get a member by name

attributes allow us to pass more metadata to types, then we can look up, read these metadata and do some stuff

you can restrict the usage of your attributes using [AttributeUsage(AttributeTargets.Method)] or like this [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
if you don't restrict, you can use it anywhere

attributes are attached to the type and not instances, 2 separate instances won't have different attributes

Activator by default looks for public ctors so you'd need to provide binding flags for non-public ctors

Activator.CreateInstance is 40x slower than using the type's ctor, so think carefully before using reflection

with unsafe accessors you need to know the type infromation at compile time

if you truly need to load assemblies and scan for types => you gonna need reflection 