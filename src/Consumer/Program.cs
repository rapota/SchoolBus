// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using Consumer.Messages;
using Rebus.Activation;
using Rebus.Config;
using Rebus.Logging;

using var adapter = new BuiltinHandlerActivator();

adapter.Handle<Job>(async (bus, job) =>
{
    var keyChar = job.KeyChar;
    var processId = Process.GetCurrentProcess().Id;
    var reply = new Reply(keyChar, processId);

    await bus.Reply(reply);
});

Configure.With(adapter)
    .Logging(l => l.ColoredConsole(minLevel: LogLevel.Warn))
    .Transport(t => t.UseRabbitMq("amqp://guest:guest@localhost:5672", "console-subscriber"))
    .Start();

Console.WriteLine("Press ENTER to quit");
Console.ReadLine();
