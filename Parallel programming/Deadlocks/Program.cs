#region Deadlock!!

var bgThread1 = Task.Run(() => Thread.Sleep(100));
var bgThread2 = Task.Run(() => Thread.Sleep(200));

bgThread1 = Task.Run(() => SimulateLongRunningTask(bgThread2, CancellationToken.None));
bgThread2 = Task.Run(() => SimulateLongRunningTask(bgThread1, CancellationToken.None));

await bgThread1;
await bgThread2;

async Task SimulateLongRunningTask(Task otherLongRunningFunction, CancellationToken token)
{
    await Task.Delay(TimeSpan.FromSeconds(2), token);
    await otherLongRunningFunction;
    Console.WriteLine($"Finished thread {Thread.CurrentThread.ManagedThreadId}");
}

#endregion