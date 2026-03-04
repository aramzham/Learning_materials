// See https://aka.ms/new-console-template for more information

using System.Diagnostics;

Console.WriteLine("Hello, World!");

bool result = false;
int loopIterations = 0;

Trace.WriteLine($"Program started on thread {Environment.CurrentManagedThreadId}");

while (loopIterations < 1_000)
{
    var bgTask1 = Task.Run(() => SimulateLongRunningTask("Task-1", true, CancellationToken.None));
    var bgTask2 = Task.Run(() => SimulateLongRunningTask("Task-2", false, CancellationToken.None));

    await bgTask1;
    await bgTask2;
    
    loopIterations++;
}

async Task SimulateLongRunningTask(string name, bool setResult, CancellationToken cancellationToken)
{
    Trace.WriteLine($"{name} started on thread {Environment.CurrentManagedThreadId}");
    
    await Task.Delay(2_000);

    result = setResult;
    
    Trace.WriteLine($"Set {nameof(result)} to {setResult}");

    if (result != setResult)
    {
        throw new Exception("Race condition detected!!");
    }

    Console.WriteLine($"{name} completed on thread {Environment.CurrentManagedThreadId}!!");
}