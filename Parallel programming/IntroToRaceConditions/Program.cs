// See https://aka.ms/new-console-template for more information

using System.Diagnostics;

Console.WriteLine("Hello, World!");

// locks
// Lock lockingMechanism = new Lock();
var semaphore = new SemaphoreSlim(1, 1);

bool result = false;
int loopIterations = 0;
int number = 0;

Trace.WriteLine($"Program started on thread {Environment.CurrentManagedThreadId}");

while (loopIterations < 1_000)
{
    var bgTask1 = Task.Run(() => SimulateLongRunningTask("Task-1", true, CancellationToken.None));
    var bgTask2 = Task.Run(() => SimulateLongRunningTask("Task-2", false, CancellationToken.None));
    
    // var bgTask1 = Task.Run(() => SimulateLongRunningTaskWithInt("Task-1", 10, CancellationToken.None));
    // var bgTask2 = Task.Run(() => SimulateLongRunningTaskWithInt("Task-2", 20, CancellationToken.None));

    await bgTask1;
    await bgTask2;
    
    loopIterations++;
}

Console.WriteLine($"Program completed on thread {Environment.CurrentManagedThreadId}");
return;

async Task SimulateLongRunningTask(string name, bool setResult, CancellationToken cancellationToken)
{
    Trace.WriteLine($"{name} started on thread {Environment.CurrentManagedThreadId}");
    
    await Task.Delay(TimeSpan.FromMilliseconds(2), cancellationToken);

    // lock (lockingMechanism)
    // {
    //     // your synchronous code here
    // }
    
    await semaphore.WaitAsync(cancellationToken);
    try
    {
        result = setResult;
    
        Trace.WriteLine($"Set {nameof(result)} to {setResult}");

        if (result != setResult)
        {
            throw new Exception("Race condition detected!!");
        }
    }
    finally
    {
        semaphore.Release();
    }

    Trace.WriteLine($"{name} completed on thread {Environment.CurrentManagedThreadId}!!");
}

async Task SimulateLongRunningTaskWithInt(string name, int setNumber, CancellationToken cancellationToken)
{
    Trace.WriteLine($"{name} started on thread {Environment.CurrentManagedThreadId}");
    
    await Task.Delay(TimeSpan.FromMilliseconds(2), cancellationToken);
    
    Interlocked.CompareExchange(ref number, setNumber, number);

    Console.WriteLine($"{name} completed on thread {Environment.CurrentManagedThreadId}!!");
}