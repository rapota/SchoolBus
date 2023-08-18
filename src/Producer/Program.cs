// See https://aka.ms/new-console-template for more information

using Consumer.Messages;
using Rebus.Activation;
using Rebus.Config;
using Rebus.Logging;
using Rebus.Routing.TypeBased;

using var adapter = new BuiltinHandlerActivator();

adapter.Handle<Reply>(async reply =>
{
    await Console.Out.WriteLineAsync($"Got reply '{reply.KeyChar}' (from OS process {reply.OsProcessId})");
});

Configure.With(adapter)
    .Logging(l => l.ColoredConsole(minLevel: LogLevel.Warn))
    .Transport(t => t.UseRabbitMq("amqp://guest:guest@localhost:5672", "console-subscriber"))
    .Routing(r => r.TypeBased().MapAssemblyOf<Job>("console-subscriber"))
    .Start();

Console.WriteLine("Press Q to quit or any other key to produce a job");
while (true)
{
    char keyChar = char.ToLower(Console.ReadKey(true).KeyChar);
    switch (keyChar)
    {
        case 'q':
            goto quit;

        default:
            adapter.Bus.Send(new Job(keyChar)).Wait();
            break;
    }
}

quit:
Console.WriteLine("Quitting...");
