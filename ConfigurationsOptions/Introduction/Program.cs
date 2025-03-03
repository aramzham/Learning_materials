using Microsoft.Extensions.Configuration;

var builder = new ConfigurationBuilder();

// provider - builder.AddInMemoryCollection
// source - new Dictionary<string, string>
builder.AddInMemoryCollection(new Dictionary<string, string>
{
    ["MemoryKey1"] = "MemoryValue1",
    ["MemoryKey2"] = "MemoryValue2",
}!);

var config = builder.Build();

var value = config["MemoryKey1"];

Console.WriteLine(value);