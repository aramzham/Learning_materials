namespace Getting_started.Web;

public class Worker(ILogger<Worker> logger, IConfiguration config) : BackgroundService
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

            // var delay = TimeSpan.Parse(config["Delay"] ?? "00:00:03");
            var delay = config.GetValue<TimeSpan>(key: "Delay", defaultValue: TimeSpan.FromSeconds(3));
            
            await Task.Delay(delay, stoppingToken);
        }
    }
}