// See https://aka.ms/new-console-template for more information

using dotenv.net;
using RawJsonImplementation.Models;
using RawJsonImplementation.Services;

Console.WriteLine("Hello, World!");

DotEnv
    .Fluent()
    .WithProbeForEnv() // searches upward for a .env file (project root, solution root, etc.)
    .WithTrimValues()
    .Load();

var apiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY");
if (apiKey is null)
    throw new InvalidOperationException("OPENAI_API_KEY is not set");

List<ChatMessage> messages = [
    new ChatMessage()
    {
        Role = ChatRole.Assistant,
        Content = "Hello, what do you want to do today?"
    }
];

Console.WriteLine(messages[0].Content);

var aiService = new OpenAiService(apiKey);

while (true)
{
    Console.ForegroundColor = ConsoleColor.Blue;
    var input = Console.ReadLine();
    if (input is null || input.Equals("exit", StringComparison.CurrentCultureIgnoreCase))
        break;
    Console.ResetColor();

    messages.Add(new ChatMessage()
    {
        Role = ChatRole.User,
        Content = input
    });

    var response = await aiService.GetCompletion(messages, CancellationToken.None);
    messages.Add(response);
    Console.WriteLine(messages[^1].Content);
}