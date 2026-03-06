// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using TaskWhenAll;

var salad = new Salad();
var risotto = new Risotto();
var pizza = new Pizza();
var lasagna = new Lasagna();
var pasta = new Pasta();

var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(4));

Trace.WriteLine("Starting to cook.");

try
{
    await Task.WhenAll(
            salad.Cook(), 
            risotto.Cook(), 
            pizza.Cook(), 
            lasagna.Cook(), 
            pasta.Cook())
        .WaitAsync(cancellationTokenSource.Token); // waitAsync passes the cancellation token to all the tasks and if any of them is canceled it throws at your face
    
    Trace.WriteLine("Cooking completed!!");
}
catch (TaskCanceledException)
{
    Trace.WriteLine("Oh no! The guests are leaving!");
}