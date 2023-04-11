using System.Runtime.CompilerServices;
using MassTransit;

namespace ag.saga.common.Events;

public class BookingInitializationEvent
{
    public Guid BookingId { get; set; }
    public decimal Price { get; set; }
    
    MessageData<string> Document { get; set; }
    
    [ModuleInitializer]
    internal static  void Init()
    {
        GlobalTopology.Send.UseCorrelationId<BookingInitializationEvent>(x=> x.BookingId);
    }
}