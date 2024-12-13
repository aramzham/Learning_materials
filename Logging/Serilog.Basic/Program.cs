using Serilog;
using Serilog.Basic;

var logger = new LoggerConfiguration()
    .WriteTo.Console(theme: Serilog.Sinks.SystemConsole.Themes.AnsiConsoleTheme.Code)
    .WriteTo.File("log.txt")
    .Destructure.ByTransforming<Payment>(p => new { p.Amount, p.PaymentType }) // to omit the properties we don't want to expose
    .CreateLogger();

logger.Information("Hello, World!");

Log.Logger = logger;

var payment = new Payment() { PaymentType = 2, Amount = 300m, OccuredAt = DateTime.UnixEpoch };
var dict = new Dictionary<int, string>()
{
    { 1, "One" },
    { 2, "Two" },
    { 3, "Three" }
};

logger.Information("Serilog configured");
// structured logging
logger.Information("Payment data = {@Payment}", payment);
// dictionary will automatically be structure => no need for @ sign
logger.Information("Dictionary data = {Dictionary}", dict);
// to print out the type of dictionary use $ sign
logger.Information("Dictionary data = {$Dictionary}", dict);

await Log.CloseAndFlushAsync();