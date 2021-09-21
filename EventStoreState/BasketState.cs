using EventStore.ClientAPI;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventStoreState
{
    public class BasketState
    {
        public Guid Id { get; set; }

        public int Count { get; set; }

        public int LastProcessedEventNumber { get; set; }

        public void Update(ResolvedEvent evt, int lastProcessedEventNumber)
        {
            var data = evt.Event.Data;
            LastProcessedEventNumber = lastProcessedEventNumber;

            var eventCount = Encoding.UTF8.GetString(data);
            var count = JObject.Parse(eventCount)["products"].ToList();

            if (evt.Event.EventType == "ProductAdd")
            {
                Count += count.Count;

            }
            else if (evt.Event.EventType == "ProductRemove")
            {
                Count -= count.Count;
            }

            Console.WriteLine($"current account in the basket: {Count}, event number {LastProcessedEventNumber}");
        }
    }
}
