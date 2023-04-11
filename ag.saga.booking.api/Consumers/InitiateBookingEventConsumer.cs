using System.Text.Json;
using ag.saga.common.Events;
using MassTransit;

namespace ag.saga.booking.api.Consumers;

public class InitiateBookingEventConsumer : ConsumerBase<BookingInitializationEvent>
{
    protected override Task ConsumeInternal(ConsumeContext<BookingInitializationEvent> context)
    {
        var msg = JsonSerializer.Serialize(context.Message);
        Console.WriteLine($"New Booking initiated {msg}");
        return Task.CompletedTask;
    }
}