using EventStore.ClientAPI;
using EventStoreApp.Core;
using System;
using System.Text;
using System.Threading.Tasks;

namespace EventStoreApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using var connection = await EventStoreHelpers.CreateConnection();

            var stream = "cool-events";

            await connection.SubscribeToStream(stream, ProcessEvent);

            Console.WriteLine("processing events");
            Console.ReadLine();
        }

        static void ProcessEvent(
            EventStoreSubscription subscriptions,
            ResolvedEvent resolvedEvent)
        {
            var evt = resolvedEvent.Event;

            Console.WriteLine(evt.EventStreamId);
            Console.WriteLine(evt.EventType);

            var jsonData = Encoding.UTF8.GetString(evt.Data);
            Console.WriteLine(jsonData);
        }
    }
}
