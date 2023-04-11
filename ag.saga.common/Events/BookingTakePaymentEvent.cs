using System.Runtime.CompilerServices;
using MassTransit;

namespace ag.saga.common.Events;

public class BookingTakePaymentEvent
{

    public BookingTakePaymentEvent(Guid bookingId)
    {
        BookingId = bookingId;
    }
    
    public Guid BookingId { get; set; }
    public decimal Price { get; set; }
    
    [ModuleInitializer]
    internal static  void Init()
    {
        GlobalTopology.Send.UseCorrelationId<BookingTakePaymentEvent>(x=> x.BookingId);
    }
}