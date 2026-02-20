using Azure;
using Azure.AI.OpenAI;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MySecondChat;

var host = Host.CreateApplicationBuilder();

host.Configuration.AddUserSecrets<IAssemblyMarker>();

var endpoint = host.Configuration["Chat:AzureOpenAI:Endpoint"];
var key = host.Configuration["Chat:AzureOpenAI:ApiKey"];

host.Services.AddChatClient(_ => new AzureOpenAIClient(new Uri(endpoint), new AzureKeyCredential(key))
    .GetChatClient("gpt-4o-mini")
    .AsIChatClient());

host.Services.AddHttpClient(); 

// host.Services.AddHostedService<ChatApp>();
host.Services.AddHostedService<WebChatApp>();

var app = host.Build();

await app.RunAsync();

Console.WriteLine($"{endpoint}-{key}");