// See https://aka.ms/new-console-template for more information

using System.Diagnostics;

Console.WriteLine("Hello, World!");

IReadOnlyList<int> iterationList = [.. Enumerable.Range(1, 10_000)];
var stopwatch = Stopwatch.StartNew();

// retrieve prime numbers sequentially
var primeNumbers = iterationList.Select(FindNextPrime).ToList();

stopwatch.Stop();

Console.WriteLine($"***Finding prime numbers in series took {stopwatch.Elapsed.TotalSeconds:F3}s.");

Console.WriteLine($"Finding prime numbers in parallel using PLINQ...");
stopwatch.Restart();

// retrieve prime numbers in parallel
var cts = new CancellationTokenSource(TimeSpan.FromSeconds(60));
var parallelPrimeNumbers = iterationList
    .AsParallel()
    .AsOrdered()
    .WithCancellation(cts.Token)
    .WithDegreeOfParallelism(Environment.ProcessorCount)
    .WithExecutionMode(ParallelExecutionMode.ForceParallelism)
    .WithMergeOptions(ParallelMergeOptions.NotBuffered)
    .Select(FindNextPrime)
    .ToList();

stopwatch.Stop();

Console.WriteLine($"***Finding prime numbers in parallel took {stopwatch.Elapsed.TotalSeconds:F3}s.");

foreach (var p in parallelPrimeNumbers)
{
    Console.Write($"{p}, ");
}

return;

static long FindNextPrime(int number)
{
    var count = 0;
    long a = 2;
    while (count < number)
    {
        var isPrime = true;
        for (long b = 2; b * b <= a; b++)
        {
            if (a % b == 0)
            {
                isPrime = false;
                break;
            }
        }

        if (isPrime)
        {
            count++;
            if (count == number)
            {
                return a;
            }
        }

        a++;
    }

    return --a;
}