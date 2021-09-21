using EventStore.ClientAPI;
using System;
using System.Threading.Tasks;

namespace EventStoreApp.Core
{
    public static class EventStoreExtentions
    {
        public static async Task<EventStoreSubscription> SubscribeToStream(
            this IEventStoreConnection connection,
            string stream,
            Action<EventStoreSubscription, ResolvedEvent> processEvent)
        {
            return await connection.SubscribeToStreamAsync(
                stream,
                false,
                processEvent,
                null,
                EventStoreHelpers.GetCredentials());
        }
    }
}
