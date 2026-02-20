using Microsoft.Extensions.AI;

namespace  MySecondChat;

using Microsoft.Extensions.Hosting;

public class ChatApp(IHostApplicationLifetime lifetime, IChatClient ai) : BackgroundService
{
    private bool _exitRequested = false;
    private List<ChatMessage> _history = [];
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Console.CancelKeyPress += (sender, args) =>
        {
            Console.WriteLine("Exit key pressed.");
            args.Cancel = true;
            _exitRequested = true;
            lifetime.StopApplication();
        };
        
        var systemMessage = new ChatMessage(ChatRole.System, "You are a helpful AI assistant that tries to answer the user's query.");
        _history.Add(systemMessage);
        var response = await ai.GetResponseAsync(systemMessage, cancellationToken: stoppingToken);
        Console.WriteLine($"AI: {response.Text}");
        
        while (!stoppingToken.IsCancellationRequested)
        {
            Console.Write("Prompt > ");
            var input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input) || _exitRequested)
            {
                break;
            }

            _history.Add(new ChatMessage(ChatRole.User, input));
            response = await ai.GetResponseAsync(_history, cancellationToken: stoppingToken);
            _history.AddMessages(response);
            foreach (var msg in response.Messages)
            {
                Console.WriteLine($"{msg.Role}: {msg.Text}");
            }
        }
        Console.WriteLine("Goodbye!");
    }
}