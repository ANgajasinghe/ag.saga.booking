using MassTransit;

namespace ag.saga.booking.api.Consumers;

public abstract class ConsumerBase<T> : IConsumer<T> where T : class
{
    public async Task Consume(ConsumeContext<T> context)
    {

        try
        {
            await ConsumeInternal(context);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    protected abstract Task ConsumeInternal(ConsumeContext<T> context);
}