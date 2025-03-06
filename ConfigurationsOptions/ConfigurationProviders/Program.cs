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

Console.WriteLine(builder.Configuration.GetDebugView());