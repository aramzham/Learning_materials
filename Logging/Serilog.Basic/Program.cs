using Serilog;

var logger = new LoggerConfiguration()
    .WriteTo.Console(theme: Serilog.Sinks.SystemConsole.Themes.AnsiConsoleTheme.Code)
    .WriteTo.File("log.txt")
    .CreateLogger();

logger.Information("Hello, World!");

Log.Logger = logger;

await Log.CloseAndFlushAsync();