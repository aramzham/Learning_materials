using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

builder.Host.UseSerilog();

var app = builder.Build();

app.MapGet("/", (ILogger<Program> logger) => logger.LogInformation("Hello World!"));

app.Run();