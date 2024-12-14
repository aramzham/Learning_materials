using Microsoft.Extensions.Logging;

namespace Logging.HighPerformance;

public static partial class LoggerExtensions
{
    [LoggerMessage(EventId = 1,
        Level = LogLevel.Information,
        Message = "Payment created for user {email} for product {productId} with amount {amount}")]
    public static partial void LogPaymentCreation(this ILogger logger, string email, decimal amount, int productId);
}