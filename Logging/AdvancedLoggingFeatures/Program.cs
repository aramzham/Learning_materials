using System.Text.Json;
using AdvancedLoggingFeatures;
using Microsoft.Extensions.Logging;

using var loggerFactory = LoggerFactory.Create(builder =>
{
    builder.SetMinimumLevel(LogLevel.Information);
    builder.AddJsonConsole(x =>
    {
        x.JsonWriterOptions = new JsonWriterOptions
        {
            Indented = true
        };
    });
});

ILogger logger = loggerFactory.CreateLogger<Program>();

var paymentId = 1;
var amount = 15.99;

using (logger.BeginTimedOperation("Handling a payment at {Date}", DateTime.Now))
{
    logger.LogInformation(
        "New Payment with id {PaymentId} for ${Total}", paymentId, amount);
    await Task.Delay(1000);

}

