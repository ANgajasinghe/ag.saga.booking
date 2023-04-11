using System.Text.Json;
using ag.saga.common.Events;
using MassTransit;

namespace ag.saga.booking.api.Consumers;

public class BookingTakePaymentEventConsumer : ConsumerBase<BookingTakePaymentEvent>
{
    protected override  Task ConsumeInternal(ConsumeContext<BookingTakePaymentEvent> context)
    {
        var  msg = JsonSerializer.Serialize(context.Message);
        Console.WriteLine($"Booking Take Payment Event {msg}");
        return Task.CompletedTask;
    }
}