using ConsumerApp;
using Vax;

var services = new ServiceCollection();

services.AddSingleton<IConsoleWriter, ConsoleWriter>();
// services.AddTransient<ConsoleWriter>();
// services.AddSingleton(new ConsoleWriter());
// services.AddSingleton<IIdGenerator, IdGenerator>();
// services.AddTransient<IIdGenerator, IdGenerator>();
services.AddTransient(provider => new IdGenerator(provider.GetService<IConsoleWriter>()));

var serviceProvider = services.BuildServiceProvider();

var service = serviceProvider.GetService<IConsoleWriter>();
service?.WriteLine("Hello, World!");

var idGenerator1 = serviceProvider.GetService<IdGenerator>();
var idGenerator2 = serviceProvider.GetService<IdGenerator>();
idGenerator1?.PrintId();
idGenerator2?.PrintId();