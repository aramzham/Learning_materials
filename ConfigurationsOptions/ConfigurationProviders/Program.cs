using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateEmptyApplicationBuilder(null);

// JSON {;}
builder.Configuration.AddJsonFile(
    path: "appsettings.json",
    optional: false,
    reloadOnChange: true);

builder.Configuration.AddJsonFile(
    path: $"appsettings.{builder.Environment.EnvironmentName}.json",
    optional: true,
    reloadOnChange: true);

// XML </>

builder.Configuration.AddXmlFile(
    path: "config.xml",
    optional: true,
    reloadOnChange: true
    );
    
builder.Configuration.AddXmlFile(
    path: $"config.{builder.Environment.EnvironmentName}.xml",
    optional: true,
    reloadOnChange: true
);

// INI [=]
builder.Configuration.AddIniFile(
    path: "app.ini",
    optional: true,
    reloadOnChange: true
    );
builder.Configuration.AddIniFile(
    path: $"app.{builder.Environment.EnvironmentName}.ini",
    optional: true,
    reloadOnChange: true
);

// user secrets (?!)
builder.Configuration.AddUserSecrets<Program>(
    optional: false,
    reloadOnChange: true
    );

// environment variables ($env)

// builder.Configuration.AddEnvironmentVariables();
// many .net specific variables are automatically pulled in from the Microsoft.Extensions.Hosting meta-package defaults. Such as, but not limited to, the "DOTNET_*" prefixed env vars.

builder.Configuration.AddEnvironmentVariables(prefix: "MYAPP_");

// command-line args (-a)
// Microsoft.Extensions.Configuration.CommandLine

var switchMappings = new Dictionary<string, string>()
{
    // -a="something" or -a "something"
    ["-d"] = "MyApp:MyKey",
    // --apiOn=true /apiOn=true
    ["--apiOn"] = "TodoApiOptions:Enabled",
    // --todoUri <uri> /todoUri <uri>
    ["--todoUri"] = "TodoApiOptions:BaseAddress"
};

builder.Configuration.AddCommandLine(args, switchMappings);

// key-per file (/key)
// Microsoft.Extensions.Configuration.KeyPerFile

builder.Configuration.AddKeyPerFile(directoryPath: @"C:\Users\Aram\source\repos\Learning_materials\ConfigurationsOptions\ConfigurationProviders\key_per_file", optional: true);

Console.WriteLine(builder.Configuration.GetDebugView());