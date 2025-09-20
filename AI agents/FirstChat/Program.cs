using dotenv.net;
using OpenAI.Chat;

DotEnv.Load();
var openAiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY");
if (openAiKey is null)
    throw new Exception("OPENAI_API_KEY environment variable not set");
    
var client = new ChatClient(model: "gpt-5-nano", apiKey: openAiKey);

List<ChatMessage> messages = [
    new AssistantChatMessage("Hello, what do you want to do today?")
];

Console.WriteLine(messages[0].Content[0].Text);

while (true)
{
    Console.ForegroundColor = ConsoleColor.Blue;
    var input = Console.ReadLine();
    if (input is null || input?.ToLower() == "exit")
        break;
    Console.ResetColor();
    
    messages.Add(new UserChatMessage(input));

    var completion = await client.CompleteChatAsync(messages);
    
    var response = completion.Value.Content[0].Text;
    
    messages.Add(new AssistantChatMessage(response));
    Console.WriteLine(response);
}