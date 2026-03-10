// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using ParallelFor;

Console.WriteLine("Hello, World!");

var salad = new Salad();
var risotto = new Risotto();
var pizza = new Pizza();
var lasagna = new Lasagna();
var pasta = new Pasta();

const int numOfOrdersSalad = 10;
const int numOfOrdersRisotto = 3;
const int numOfOrdersPizza = 5;
const int numOfOrdersLasagna = 2;
const int numOfOrdersPasta = 1;

var cts = new CancellationTokenSource(TimeSpan.FromSeconds(40));
var parallelOptions = new ParallelOptions()
{
    CancellationToken = cts.Token,
    MaxDegreeOfParallelism = 50
};

Trace.WriteLine("Starting to cook.");

try
{
    var saladCookingTask = Parallel.ForAsync(0, numOfOrdersSalad, parallelOptions, async (orderNumber, token) => await salad.Cook(orderNumber, token));
    var risottoCookingTask = Parallel.ForAsync(0, numOfOrdersRisotto, parallelOptions, async (orderNumber, token) => await risotto.Cook(orderNumber, token));
    var pizzaCookingTask = Parallel.ForAsync(0, numOfOrdersPizza, parallelOptions,
        async (orderNumber, token) => await pizza.Cook(orderNumber, token));
    var lasagnaCookingTask = Parallel.ForAsync(0, numOfOrdersLasagna, parallelOptions, async (orderNumber, token) => await lasagna.Cook(orderNumber, token));
    var pastaCookingTask = Parallel.ForAsync(0, numOfOrdersPasta, parallelOptions, async (orderNumber, token) => await pasta.Cook(orderNumber, token));
    
    await Task.WhenAll(saladCookingTask, risottoCookingTask, pizzaCookingTask, lasagnaCookingTask, pastaCookingTask);
}
catch (OperationCanceledException e)
{
    Trace.WriteLine(e.Message);
    Trace.WriteLine("Some dishes might be missing.");
}

Trace.WriteLine("Done.");

