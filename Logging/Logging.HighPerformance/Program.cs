using Logging.HighPerformance;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using var host = Host.CreateDefaultBuilder(args)
    .ConfigureLogging(x =>
    {
        x.ClearProviders();
        x.AddConsole();
    })
    .ConfigureServices((context, services) => { services.AddSingleton<PaymentService>(); })
    .Build();

var paymentService = host.Services.GetRequiredService<PaymentService>();
paymentService.Create("some@email.com", 13.3m, 10);