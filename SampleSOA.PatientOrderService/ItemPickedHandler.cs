using System;

using MassTransit;

using SampleSOA.Messages;

namespace SampleSOA.PatientOrderService
{
    public class ItemPickedHandler : BaseRetryHandler<ItemPicked>
        //: Consumes<ItemPicked>.All
    {
        private static readonly Random _random = new Random();

        protected override void Handle(ItemPicked message)
        {
            if (_random.Next(3) > 0)
            {
                Console.WriteLine(
                    "[{0}] {1} Picked {2} units of item#{3} at {4} by {5}.",
                    message.CorrelationId,
                    message.EventTime,
                    message.Quantity,
                    message.ItemId,
                    message.AtDeviceId,
                    message.ByUserId);
            }
            else
            {
                Console.WriteLine("Failing message {0}",
                    message.CorrelationId);
                throw new ArithmeticException("blah blah blah");
            }
        }
    }
}