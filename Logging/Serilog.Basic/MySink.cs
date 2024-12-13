using Serilog.Core;
using Serilog.Events;

namespace Serilog.Basic;

public class MySink(IFormatProvider? formatProvider) : ILogEventSink
{
    private IFormatProvider? _formatProvider = formatProvider;

    public MySink() : this(null)
    {
        
    }
    
    public void Emit(LogEvent logEvent)
    {
        var message = logEvent.RenderMessage(_formatProvider);
        Console.BackgroundColor = ConsoleColor.DarkBlue;
        Console.WriteLine($"[{DateTime.Now}] - {message}");
        Console.ResetColor();
    }
}