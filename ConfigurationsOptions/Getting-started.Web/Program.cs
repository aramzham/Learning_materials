using Getting_started.Web;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHostedService<Worker>();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

await app.RunAsync();