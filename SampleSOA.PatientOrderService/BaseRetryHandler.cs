using MassTransit;

namespace SampleSOA.PatientOrderService
{
    public abstract class BaseRetryHandler<TMessage> : Consumes<TMessage>.All 
        where TMessage : class
    {
        public void Consume(TMessage message)
        {
            try
            {
                Handle(message);
            }
            catch
            {
                this.MessageContext().RetryLater();
            }
        }

        protected abstract void Handle(TMessage message);
    }
}