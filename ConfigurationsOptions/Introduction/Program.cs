using Microsoft.Extensions.Configuration;

var builder = new ConfigurationBuilder();

// provider - builder.AddInMemoryCollection
// source - new Dictionary<string, string>
builder.AddInMemoryCollection([
    new KeyValuePair<string, string?>("Key", "123 that was easy..."),
    new KeyValuePair<string, string?>("Nested:Sub:Section", Math.PI.ToString()),
    new KeyValuePair<string, string?>("ConnectionStrings:Example", "my_db_cs"),
    new KeyValuePair<string, string?>("MemoryKey1", "mKv")
]);

// sources
foreach (var source in builder.Sources)
{
    Console.WriteLine($"""
                       Source: 
                            {source}
                       """);
}

var config = builder.Build();

var value = config["MemoryKey1"];

Console.WriteLine(value);

// providers
foreach (var provider in config.Providers)
{
    Console.WriteLine($"""
                       Provider:
                            {provider}
                       """);
}

// recuse config sections

RecurseConfigSections(config);
static void RecurseConfigSections(IConfiguration section, string? parentKey = null)
{
    foreach (var child in section.GetChildren())
    {
        var key = parentKey is null ? child.Key : $"{parentKey}:{child.Key}";
        
        Console.WriteLine($"{key}: {child.Value}");
        
        RecurseConfigSections(child, key);
    }
}

// nested sub section
// could use the indexer as well
var section = config.GetSection("Nested:Sub:Section");
Console.WriteLine($"""
                   Nested:Sub:Section:
                        {section.Value}
                   """);
Console.WriteLine();

// connection string
var connectionString = config.GetConnectionString("Example");
Console.WriteLine($"""
                   ConnectionString:
                        {connectionString}
                   """);
Console.WriteLine();

// debug view
Console.WriteLine($"DebugView\n{config.GetDebugView()}");