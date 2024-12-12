using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace AdvancedLoggingFeatures;

public class TimedOperation : IDisposable
{
    private readonly object?[] _args;
    private readonly long _startingTimestamp;
    private readonly ILogger _logger;
    private readonly LogLevel _logLevel;
    private readonly string _messageTemplate;
    
    public TimedOperation(
        ILogger logger,
        LogLevel logLevel,
        string messageTemplate,
        params object?[] args)
    {
        _startingTimestamp = Stopwatch.GetTimestamp();
        _logger = logger;
        _logLevel = logLevel;
        _messageTemplate = messageTemplate;
        _args = new object[args.Length + 1];
        Array.Copy(args, _args, args.Length);
    }
    
    public void Dispose()
    {
        var delta = Stopwatch.GetElapsedTime(_startingTimestamp);
        _args[^1] = delta.TotalMilliseconds;
        _logger.Log(_logLevel, $"{_messageTemplate} completed in {{OperationDurationMs}}ms", _args);
    }
}