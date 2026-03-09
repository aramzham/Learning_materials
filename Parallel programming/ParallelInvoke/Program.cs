// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using ParallelInvoke;

Console.WriteLine("Hello, World!");

var salad = new Salad();
var risotto = new Risotto();
var pizza = new Pizza();
var lasagna = new Lasagna();
var pasta = new Pasta();

var cts = new CancellationTokenSource(TimeSpan.FromSeconds(40));
var parallelOptions = new ParallelOptions()
{
    CancellationToken = cts.Token,
    MaxDegreeOfParallelism = 2
};

Trace.WriteLine("Starting to cook.");

try
{
    Parallel.Invoke(parallelOptions, 
        () => salad.Cook(), 
        risotto.Cook, 
        pizza.Cook, 
        lasagna.Cook, 
        pasta.Cook);
}
catch (OperationCanceledException e)
{
    Trace.WriteLine(e.Message);
    Trace.WriteLine("Some dishes might be missing.");
}

Trace.WriteLine("Done.");

