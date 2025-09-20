﻿using ConsoleAgentWithMicrosoftAI;
using dotenv.net;
using Microsoft.Extensions.Hosting;

DotEnv
    .Fluent()
    .WithProbeForEnv() // searches upward for a .env file (project root, solution root, etc.)
    .WithTrimValues()
    .Load();
    
var provider = "openai";
var model = "gpt-5-mini";
for (int i = 0; i < args.Length; i++)
{
    if (args[i] == "--provider" && i + 1 < args.Length)
    {
        provider = args[i + 1].ToLower();
    }
    else if (args[i] == "--model" && i + 1 < args.Length)
    {
        model = args[i + 1];
    }
}

var builder = Host.CreateApplicationBuilder(args);
Startup.ConfigureServices(builder, provider, model);
var host = builder.Build();

await ChatAgent.RunAsync(host.Services);