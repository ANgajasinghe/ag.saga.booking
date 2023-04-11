using ag.saga.booking.state_machine;
using MassTransit;
using MassTransit.MongoDbIntegration.MessageData;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMassTransit(x =>
{
    x.AddSagaStateMachine<BookingStateMachine, BookingState>()
        .MongoDbRepository(r =>
        {
            r.Connection = "mongodb://127.0.0.1/27017";
            r.DatabaseName = "statemachine";
            r.CollectionName = "bookingstate";
        });

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost",  "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        cfg.UseMessageData(new MongoDbMessageDataRepository("mongodb://127.0.0.1/27017", "statemachine"));
        cfg.ConfigureEndpoints(context);
    });
});


var app = builder.Build();

app.MapGet("/", () => "Hello World!");



app.Run();