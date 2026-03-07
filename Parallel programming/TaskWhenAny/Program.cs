// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using TaskWhenAny;

var salad = new Salad();
var risotto = new Risotto();
var pizza = new Pizza();
var lasagna = new Lasagna();
var pasta = new Pasta();

var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(5));

Trace.WriteLine("Starting to cook.");

List<Task<string>> tasks =
[
    salad.Cook(cancellationTokenSource.Token),
    risotto.Cook(cancellationTokenSource.Token),
    pizza.Cook(cancellationTokenSource.Token),
    lasagna.Cook(cancellationTokenSource.Token),
    pasta.Cook(cancellationTokenSource.Token)
];

try
{
    while (tasks.Count > 0)
    {
        Task<string> completedTask = await Task.WhenAny(tasks).WaitAsync(cancellationTokenSource.Token);

        var food = await completedTask;
        
        Trace.WriteLine($"Eating {food}");

        tasks.Remove(completedTask);
    }
    
    Trace.WriteLine("Cooking completed!!");
}
catch (TaskCanceledException)
{
    Trace.WriteLine("Oh no! The guests are leaving!");
}