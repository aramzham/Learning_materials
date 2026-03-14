// See https://aka.ms/new-console-template for more information

using System.Threading.Channels;
using Channels;

Console.WriteLine("Hello, World!");

const int numTables = 5;

var restaurantOpenDuration = TimeSpan.FromSeconds(10);

IReadOnlyList<Food> menu = [
    new Lasagna(),
    new Lasagna(),
    new Salad(),
    new Pizza(),
    new Pizza(),
    new Risotto(),
    new Pasta()
];

var options = new BoundedChannelOptions(numTables)
{
    AllowSynchronousContinuations = true,
    SingleReader = true,
    SingleWriter = false,
    FullMode = BoundedChannelFullMode.Wait
};
var orderChannel = Channel.CreateBounded<Food>(options);

Console.WriteLine("Restaurant Opened");
var restaurantOpenedTime = DateTimeOffset.UtcNow;

var kitchenTask = KitchenTask();

while (restaurantOpenedTime + restaurantOpenDuration > DateTimeOffset.UtcNow)
{
    // take order 
    var food = GetOrder();
    
    var numSecondsBeforeNextOrder = Random.Shared.Next(1, 3);
    await orderChannel.Writer.WriteAsync(food);

    Console.WriteLine();
    Console.WriteLine($"Submitting order for {food.Name}. Total unread orders: {orderChannel.Reader.Count}");
    Console.WriteLine();
    
    // wait for next order
    await Task.Delay(TimeSpan.FromSeconds(numSecondsBeforeNextOrder));
}

Console.WriteLine();
Console.WriteLine($"Restaurant closing with {orderChannel.Reader.Count} orders remaining to cook in the kitchen");
Console.WriteLine();

_ = orderChannel.Writer.TryComplete();

await kitchenTask;

Console.WriteLine();
Console.WriteLine("Kitchen closed");
Console.WriteLine();
return;

Food GetOrder()
{
    var random = Random.Shared.Next(0, menu.Count);
    return menu[random];
}

async Task KitchenTask(CancellationToken ct = default)
{
    await foreach (var item in orderChannel.Reader.ReadAllAsync(ct))
    {
        Console.WriteLine();
        Console.WriteLine($"Kitchen: Cooking {item.Name}");
        Console.WriteLine();
        await item.Cook(ct);
    }
}