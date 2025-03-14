﻿using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Getting_started;

public class Worker(ILogger<Worker> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            if (logger.IsEnabled(LogLevel.Information))
            {
                logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            }
            
            if (Random.Shared.Next(100) < 30 &&
                logger.IsEnabled(LogLevel.Warning))
            {
                logger.LogWarning("seeing this roughly 30% of the time...");
            }
            
            await Task.Delay(1000, stoppingToken);
        }
    }
}