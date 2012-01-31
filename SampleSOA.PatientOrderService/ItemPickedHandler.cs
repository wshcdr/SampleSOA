using System;

using MassTransit;

using SampleSOA.Messages;

namespace SampleSOA.PatientOrderService
{
    public class ItemPickedHandler : Consumes<ItemPicked>.All
    {
        public void Consume(ItemPicked message)
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
    }
}