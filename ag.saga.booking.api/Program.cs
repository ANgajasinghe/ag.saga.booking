using ag.saga.booking.api.Consumers;
using MassTransit;
using MassTransit.MongoDbIntegration.MessageData;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });
        cfg.UseMessageData(new MongoDbMessageDataRepository("mongodb://127.0.0.1/27017", "statemachine"));
       cfg.ConfigureEndpoints(context);
    });

    x.AddConsumer<InitiateBookingEventConsumer>();
    x.AddConsumer<BookingTakePaymentEventConsumer>();
});



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