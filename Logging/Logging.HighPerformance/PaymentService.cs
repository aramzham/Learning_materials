using Microsoft.Extensions.Logging;

namespace Logging.HighPerformance;

public class PaymentService
{
    // using this you'll avoid boxing of log message parameters and the additional allocation of the parameters array
    private static Action<ILogger, string, int, decimal, Exception?> _logCreate =
        LoggerMessage.Define<string, int, decimal>(
            LogLevel.Information,
            new EventId(1, nameof(Create)),
            "Customer {Email} purchased product {ProductId} at {Amount}");

    private readonly ILogger<PaymentService> _logger;

    public PaymentService(ILogger<PaymentService> logger)
    {
        _logger = logger;
    }

    public void Create(string email, decimal amount, int productId)
    {
        // do work
        // _logger.LogInformation("Customer {Email} purchased product {ProductId} at {Amount}", email, productId, amount);
        _logCreate(_logger, email, productId, amount, null);
    }
}