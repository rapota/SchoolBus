using Rebus.Config;
using SchoolBus.Handlers;
using SchoolBus.Messages;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host
    .UseSerilog((context, config) => config.ReadFrom.Configuration(context.Configuration));

// Add services to the container.
builder.Services.AddRebus(configure =>
    {
        string? connectionString = builder.Configuration.GetConnectionString("Rabbit");
        return configure
            .Transport(t => t.UseRabbitMq(connectionString, "school-subscriber"));
    });

builder.Services.AutoRegisterHandlersFromAssemblyOf<BookSeatRequestHandler>();

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
