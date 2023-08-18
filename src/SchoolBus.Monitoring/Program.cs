using Rebus.Config;
using SchoolBus.Messages;
using SchoolBus.Monitoring;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host
    .UseSerilog((context, config) => config.ReadFrom.Configuration(context.Configuration));

// Add services to the container.
builder.Services.AutoRegisterHandlersFromAssemblyOf<SeatBookedEventHandler>();

builder.Services.AddRebus(configure =>
    {
        string? connectionString = builder.Configuration.GetConnectionString("Rabbit");
        return configure
            .Transport(t => t.UseRabbitMq(connectionString, "school-subscriber"));
    },
    onCreated: async bus =>
    {
        await bus.Subscribe<SeatBookedEvent>();
    }
);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
