using ag.saga.common.Events;
using MassTransit;

namespace ag.saga.booking.state_machine;

public class BookingState : SagaStateMachineInstance, ISagaVersion
{
    public Guid CorrelationId { get; set; }
    public int CurrentState { get; set; }
    public DateTime StateDate { get; set; }
    public DateTime? PaymentDate { get; set; }
    public int Version { get; set; }
}

public class BookingStateMachine : MassTransitStateMachine<BookingState>
{
    // Events
    public Event<BookingInitializationEvent> BookingInitializationEvent { get; }
    public Event<BookingTakePaymentEvent> BookingTakePaymentEvent { get; }


    // State

    public State BookingPaymentState { get; }
    public State ApproveState { get; }

    public BookingStateMachine()
    {
        Event(() => BookingInitializationEvent);
        Event(() => BookingTakePaymentEvent);
        InstanceState(x => x.CurrentState, BookingPaymentState, ApproveState);


        Initially(
            When(BookingInitializationEvent)
                .Then(x => x.Saga.StateDate = DateTime.Now)
                .ThenAsync(context =>
                {
                    return Console.Out.WriteLineAsync(
                        $"Booking Initiated : {context.Saga.CorrelationId} started at {context.Saga.StateDate}");
                })
                .TransitionTo(BookingPaymentState)
                 //.Publish(context => new BookingTakePaymentEvent(context.Saga.CorrelationId))
                //.Finalize()
        );


        // Flow

        During(BookingPaymentState,
            When(BookingTakePaymentEvent)
                .Then(x => x.Saga.PaymentDate = DateTime.Now)
                .ThenAsync(context =>
                {
                    return Console.Out.WriteLineAsync(
                        $"Payment Initiated : {context.Saga.CorrelationId} started at {context.Saga.PaymentDate}");
                })
                .TransitionTo(ApproveState)
                .Finalize()
        );
        
        // During(ApproveState, Ignore(BookingInitializationEvent));
        //
        // SetCompleted(async instance =>
        // {
        //     var currentState = await this.GetState(instance);
        //     return ApproveState.Equals(currentState);
        // });
    }
}