using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.AI;

namespace ConsoleAgentWithMicrosoftAI;

public static class ChatAgent
{
    public static async Task RunAsync(IServiceProvider sp)
    {
        var client = sp.GetRequiredService<IChatClient>();
        var chatOptions = sp.GetRequiredService<ChatOptions>();

        var history = new List<ChatMessage>()
        {
            new(ChatRole.System, "You are a helpful CLI assistant. Use the provided functions when appropriate. If a tool call fails due to some invalid arguments, then make an attempt to fix it by using your best judgement, then try the tool call again.")
        };

        Console.WriteLine("Ask me anything (empty = exit).");

        while (true)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            var input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input))
                break;
            Console.ResetColor();

            if (input == "/history")
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                foreach (var message in history)
                {
                    switch (message.Role.Value)
                    {
                        case "user":
                        {
                            Console.WriteLine($"USER: {message.Text}");
                            continue;
                        }
                        case "assistant" when !string.IsNullOrWhiteSpace(message.Text):
                        {
                            Console.WriteLine($"AI: {message.Text}");
                            continue;
                        }
                        case "assistant" when message.Contents.Any():
                        {
                            Console.WriteLine($"REQUEST: {JsonSerializer.Serialize(message.Contents[0])}");
                            continue;
                        }
                        case "tool":
                        {
                            Console.WriteLine($"TOOL: {JsonSerializer.Serialize(message.Contents[0])}");
                            continue;
                        }
                    }
                }
                Console.ResetColor();
                continue;
            }

            history.Add(new ChatMessage(ChatRole.User, input));
            
            var response = await client.GetResponseAsync(history, chatOptions);

            Console.WriteLine(response.Text);
            history.AddRange(response.Messages);
        }
    }
}