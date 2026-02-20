using HtmlAgilityPack;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.Hosting;

namespace MySecondChat;

public partial class WebChatApp(IHostApplicationLifetime lifetime, IChatClient ai, HttpClient httpClient) : BackgroundService
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
        
        var systemMessage = new ChatMessage(ChatRole.System, _summarizationPrompt);
        systemMessage.Contents.Add(new TextContent("number_of_sentences=4"));
        _history.Add(systemMessage);
        
        // add parsing of the web page
        var data = await httpClient.GetStringAsync("https://xn----etbqgrg5bs.xn--p1ai/igry-2010-yh/sezon-2018/11032018-vtoraya-igra-vesenney-serii#rau5", stoppingToken);
        var html = new HtmlDocument();
        html.LoadHtml(data);
        var ps = html.DocumentNode.SelectNodes("//p");
        var text = string.Join("\n", ps.Select(p => p.InnerText));
        
        _history.Add(new ChatMessage(ChatRole.Assistant, text));
        var response = await ai.GetResponseAsync(systemMessage, cancellationToken: stoppingToken);

        Console.WriteLine("-------------------------------");
        Console.WriteLine($"AI: {response.Text}");
        Console.WriteLine($"Tokens used: in={response.Usage.InputTokenCount}, out={response.Usage.OutputTokenCount}");
        
        _history.AddMessages(response);
        
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